using System.Linq.Expressions;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

     Task<TEntity> GetByIdAsync(int id,string includeProperties = "");

    Task AddAsync(TEntity entity);

    Task Delete(int id);
    void Update(TEntity entityToUpdate);
}