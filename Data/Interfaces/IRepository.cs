using System.Linq.Expressions;
using Data.Entities;

namespace Data.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "");

     Task<TEntity> GetById(int id);

    Task AddAsync(TEntity entity);

    void Delete(int id);
    void Delete(TEntity entityToDelete);

    void Update(TEntity entityToUpdate);
}