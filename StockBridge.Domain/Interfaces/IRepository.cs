namespace StockBridge.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, string keyName);
    Task<T?> GetByIdAsync(long id, string keyName);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetPagedAsync(int pageNo, int pageSize);
    Task<IEnumerable<T>> GetByFieldAsync(string fieldName, object value);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id, string keyName);
    Task DeleteAsync(long id, string keyName);
    Task DeletePermanentAsync(int id, string keyName);
    Task DeletePermanentAsync(long id, string keyName);
    Task<T?> SearchByFieldAsync(string fieldName, string searchString);
    Task<bool> IsExistByIdAsync(int id, string keyName);
    Task<bool> IsExistByIdAsync(long id, string keyName);
    Task<bool> IsReferenceAsync(int id, string tableName, string columnName);
    Task<bool> IsReferenceAsync(long id, string tableName, string columnName);
}