using DysonContext.Interface;

using Microsoft.EntityFrameworkCore;

namespace DysonContext.Repository;

public class BaseRepository<T> : IBaseRepository<T>
where T : class
{
    private readonly IContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(IContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    // 🔎 Buscar por Id
    public async Task<T?> GetItemAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    // 📋 Listar todos
    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    // ➕ Adicionar
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    // ✏️ Atualizar
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    // ❌ Remover
    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    // 🔍 Consultas customizadas
    public IQueryable<T> Query()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<ICollection<T>> GetPaginatedAsync(int take = 10, int skip = 10, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Skip(skip).Take(take).ToListAsync(cancellationToken);
    }
}
