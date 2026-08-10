using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockBridge.API.Data;
using StockBridge.Domain.Interfaces;
using System.Linq.Expressions;

namespace StockBridge.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly StockBridgeDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private readonly ILogger<Repository<TEntity>> _logger;

    public Repository(StockBridgeDbContext context, ILogger<Repository<TEntity>> logger)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
        _logger = logger;
    }

    public async Task<TEntity?> GetByIdAsync(int id, string keyName)
    {
        try
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(GetByIdAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
    }

    public async Task<TEntity?> GetByIdAsync(long id, string keyName)
    {
        try
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(GetByIdAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(GetAllAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
    }

    public async Task<IEnumerable<TEntity>> GetPagedAsync(int pageNo, int pageSize)
    {
        try
        {
            return await _dbSet
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(GetPagedAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
    }

    public async Task<IEnumerable<TEntity>> GetByFieldAsync(string fieldName, object value)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(GetByFieldAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
    }

    public async Task<TEntity?> SearchByFieldAsync(string fieldName, string searchString)
    {
        try
        {
            var result = await GetByFieldAsync(fieldName, searchString);
            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(SearchByFieldAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(AddAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
    }

    public async Task UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(UpdateAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
    }

    public async Task DeleteAsync(int id, string keyName)
    {
        try
        {
            var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
            if (entity == null)
                return;

            SetIsActive(entity, false);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(DeleteAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
    }

    public async Task DeleteAsync(long id, string keyName)
    {
        try
        {
            var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
            if (entity == null)
                return;

            SetIsActive(entity, false);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(DeleteAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
    }

    public async Task DeletePermanentAsync(int id, string keyName)
    {
        try
        {
            var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
            if (entity == null)
                return;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(DeletePermanentAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
    }

    public async Task DeletePermanentAsync(long id, string keyName)
    {
        try
        {
            var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(BuildEqualsLambda(keyName, id));
            if (entity == null)
                return;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in {nameof(DeletePermanentAsync)} for entity {typeof(TEntity).Name}");
            throw;
        }
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