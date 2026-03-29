namespace StockControl.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task SaveChangesAsync();
    void Update(T entity);
    void Remove(T entity);
}
