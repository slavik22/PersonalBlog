using System.Linq.Expressions;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly PersonalBlogDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(PersonalBlogDbContext context)
    {
        this._context = context;
        this._dbSet = context.Set<TEntity>();
    }
    
    
    public async Task<IEnumerable<TEntity>> GetAllAsync( Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split( ',', StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        
        return await query.ToListAsync();
    }

    public IEnumerable<TEntity> GetByValueAsync(Expression<Func<TEntity, bool>> find,string includeProperties = "")
    {
        IQueryable<TEntity> query =  _dbSet.Where(find);
        
        foreach (var includeProperty in includeProperties.Split( ',', StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return query.ToList();
    }
    
    public Task<TEntity> GetByValueOneAsync(Expression<Func<TEntity, bool>> find, string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;
        
        foreach (var includeProperty in includeProperties.Split( ',', StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return query.FirstOrDefaultAsync(find);
    }
    
    public async Task<TEntity> GetByIdAsync(int id, string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;
        foreach (var includeProperty in includeProperties.Split( ',', StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        
        return await query.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(TEntity entity)
    { 
        await _dbSet.AddAsync(entity);
    }

    public async Task Delete(int id)
    {
        TEntity entityToDelete = await _dbSet.FindAsync(id);
        
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
         _dbSet.Remove(entityToDelete);
    }
    public void Update(TEntity item)
    {
        var entity = _dbSet.Find(item.Id);
        if (entity == null)
        {
            throw new NullReferenceException();
        }

        _context.Entry(entity).CurrentValues.SetValues(item);
    }
}