namespace DysonContext.Interface;

public interface IBaseRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetItemAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ICollection<T>> GetPaginatedAsync(int take = 10, int skip = 10, CancellationToken cancellationToken = default);
    IQueryable<T> Query();
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
}
