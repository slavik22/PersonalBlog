using System.Linq.Expressions;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly PersonalBlogDbContext context;
    private DbSet<TEntity> dbSet;

    public GenericRepository(PersonalBlogDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }
    
    
    public async Task<IEnumerable<TEntity>> GetAll( Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public async Task<TEntity> GetById(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }

    public async void Delete(int id)
    {
        TEntity entityToDelete = await dbSet.FindAsync(id);
        Delete(entityToDelete);
    }

    public void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public void Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}