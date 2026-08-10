using Microsoft.EntityFrameworkCore;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Collections.Concurrent;
using System.Data;

namespace StockBridge.Infrastructure.Persistence;

public partial class StockBridgeDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// Caches the actual column names of each database table that has been inspected.
    /// Used to detect when a mapped entity declares columns the physical table does not have
    /// (e.g. missing migration), so those columns can be omitted from INSERT/UPDATE statements
    /// instead of failing with "Invalid column name 'X'".
    /// </summary>
    private static readonly ConcurrentDictionary<string, IReadOnlySet<string>> TableColumnsCache = new();

    public StockBridgeDbContext(DbContextOptions<StockBridgeDbContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userId = _currentUserService.UserId ?? 0;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = userId;
                entry.Entity.CreatedOn = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.ModifiedBy = userId;
                entry.Entity.ModifiedOn = DateTime.UtcNow;
            }
        }

        await ExcludeMissingColumnsAsync(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// When the physical database table is missing columns that the mapped entity declares,
    /// INSERT/UPDATE statements that reference them fail with "Invalid column name 'X'".
    /// This marks such properties as unmodified (cached per table, checked once per process),
    /// so EF simply omits them from the generated INSERT/UPDATE statements.
    /// </summary>
    private async Task ExcludeMissingColumnsAsync(CancellationToken cancellationToken)
    {
        foreach (var group in ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified)
            .GroupBy(e => e.Metadata))
        {
            var entityType = group.Key;
            var tableName = entityType.GetTableName();
            if (string.IsNullOrEmpty(tableName))
                continue;

            var schemaName = entityType.GetSchema() ?? Model.GetDefaultSchema();

            var columns = await GetTableColumnsAsync(schemaName, tableName, cancellationToken);
            if (columns == null || columns.Count == 0)
                continue;

            foreach (var entry in group)
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.IsShadowProperty())
                        continue;

                    var columnName = property.GetColumnName() ?? property.Name;
                    if (!columns.Contains(columnName))
                    {
                        entry.Property(property.Name).IsModified = false;
                    }
                }
            }
        }
    }

    private async Task<IReadOnlySet<string>?> GetTableColumnsAsync(string? schema, string table, CancellationToken cancellationToken)
    {
        var cacheKey = $"{(schema ?? "dbo")}.{table}";
        if (TableColumnsCache.TryGetValue(cacheKey, out var cached))
            return cached;

        var connection = Database.GetDbConnection();
        var openedHere = connection.State != ConnectionState.Open;
        if (openedHere)
            await connection.OpenAsync(cancellationToken);

        try
        {
            await using var command = connection.CreateCommand();
            command.CommandText = schema != null
                ? """
                    SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_SCHEMA = @schema AND TABLE_NAME = @table
                    """
                : """
                    SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_NAME = @table
                    """;

            if (schema != null)
            {
                var schemaParameter = command.CreateParameter();
                schemaParameter.ParameterName = "@schema";
                schemaParameter.Value = schema;
                command.Parameters.Add(schemaParameter);
            }

            var tableParameter = command.CreateParameter();
            tableParameter.ParameterName = "@table";
            tableParameter.Value = table;
            command.Parameters.Add(tableParameter);

            var columns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            await using var reader = await command.ExecuteReaderAsync(cancellationToken);
            while (await reader.ReadAsync(cancellationToken))
                columns.Add(reader.GetString(0));

            TableColumnsCache[cacheKey] = columns;
            return columns;
        }
        finally
        {
            if (openedHere)
                await connection.CloseAsync();
        }
    }
}