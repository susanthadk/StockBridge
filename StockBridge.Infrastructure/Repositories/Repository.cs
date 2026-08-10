using Microsoft.EntityFrameworkCore;
using StockBridge.API.Data;
using StockBridge.Domain.Interfaces;
using System.Linq.Expressions;

namespace StockBridge.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly StockBridgeDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(StockBridgeDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id, string keyName)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
    }

    public async Task<TEntity?> GetByIdAsync(long id, string keyName)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetPagedAsync(int pageNo, int pageSize)
    {
        return await _dbSet
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetByFieldAsync(string fieldName, object value)
    {
        if (value is not string stringValue)
            throw new ArgumentException("Value must be a string for StartsWith comparison.", nameof(value));

        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var property = Expression.Property(parameter, fieldName);

        if (property.Type != typeof(string))
            throw new ArgumentException($"Property '{fieldName}' must be of type string to use StartsWith.");

        var method = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) })!;
        var startsWithCall = Expression.Call(property, method, Expression.Constant(stringValue));
        var lambda = Expression.Lambda<Func<TEntity, bool>>(startsWithCall, parameter);

        return await _dbSet.AsNoTracking().Where(lambda).ToListAsync();
    }

    public async Task<TEntity?> SearchByFieldAsync(string fieldName, string searchString)
    {
        var result = await GetByFieldAsync(fieldName, searchString);
        return result.FirstOrDefault();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, string keyName)
    {
        var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
        if (entity == null)
            return;

        SetIsActive(entity, false);
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id, string keyName)
    {
        var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
        if (entity == null)
            return;

        SetIsActive(entity, false);
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePermanentAsync(int id, string keyName)
    {
        var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
        if (entity == null)
            return;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePermanentAsync(long id, string keyName)
    {
        var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
        if (entity == null)
            return;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsExistByIdAsync(int id, string keyName)
    {
        return await _dbSet.AsNoTracking().AnyAsync(BuildEqualsLambda(keyName, id));
    }

    public async Task<bool> IsExistByIdAsync(long id, string keyName)
    {
        return await _dbSet.AsNoTracking().AnyAsync(BuildEqualsLambda(keyName, id));
    }

    public async Task<bool> IsReferenceAsync(int id, string tableName, string columnName)
    {
        return await IsReferenceCoreAsync(id, tableName, columnName);
    }

    public async Task<bool> IsReferenceAsync(long id, string tableName, string columnName)
    {
        return await IsReferenceCoreAsync(id, tableName, columnName);
    }

    private async Task<bool> IsReferenceCoreAsync(object id, string tableName, string columnName)
    {
        var sql = $"SELECT CASE WHEN EXISTS (SELECT 1 FROM {tableName} WHERE {columnName} = {{0}}) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END";
        var result = await _context.Database.SqlQueryRaw<bool>(sql, id).SingleOrDefaultAsync();
        return result;
    }

    private static Expression<Func<TEntity, bool>> BuildEqualsLambda(string keyName, object value)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var property = Expression.Property(parameter, keyName);
        var constant = Expression.Convert(Expression.Constant(value), property.Type);
        var equal = Expression.Equal(property, constant);
        return Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
    }

    private static void SetIsActive(TEntity entity, bool value)
    {
        var property = typeof(TEntity).GetProperty("IsActive");
        if (property == null || property.PropertyType != typeof(bool))
            throw new InvalidOperationException($"Property 'IsActive' not found or not bool on {typeof(TEntity).Name}.");

        property.SetValue(entity, value);
    }
}