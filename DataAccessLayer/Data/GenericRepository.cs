// ***********************************************************************
// Assembly         : DataAccessLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="GenericRepository.cs" company="DataAccessLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq.Expressions;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

/// <summary>
/// Class GenericRepository.
/// Implements the <see cref="IRepository{TEntity}" />
/// </summary>
/// <typeparam name="TEntity">The type of the t entity.</typeparam>
/// <seealso cref="IRepository{TEntity}" />
public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// The context
    /// </summary>
    private readonly PersonalBlogDbContext _context;
    /// <summary>
    /// The database set
    /// </summary>
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericRepository{TEntity}" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public GenericRepository(PersonalBlogDbContext context)
    {
        this._context = context;
        this._dbSet = context.Set<TEntity>();
    }

    /// <summary>
    /// Get all as an asynchronous operation.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="includeProperties">The include properties.</param>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync( Expression<Func<TEntity, bool>>? filter = null,string includeProperties = "")
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

        return await query.ToListAsync();
    }

    /// <summary>
    /// Get by identifier as an asynchronous operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="includeProperties">The include properties.</param>
    /// <returns>A Task&lt;TEntity&gt; representing the asynchronous operation.</returns>
    public async Task<TEntity?> GetByIdAsync(int id, string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;
        foreach (var includeProperty in includeProperties.Split( ',', StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        
        return await query.FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>
    /// Add as an asynchronous operation.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddAsync(TEntity entity)
    { 
        await _dbSet.AddAsync(entity);
    }

    /// <summary>
    /// Deletes the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Task.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    public async Task Delete(int id)
    {
        TEntity entityToDelete = await _dbSet.FindAsync(id) ?? throw new InvalidOperationException();

        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
         _dbSet.Remove(entityToDelete);
    }
    /// <summary>
    /// Updates the specified item.
    /// </summary>
    /// <param name="entityToUpdate">The item.</param>
    /// <exception cref="System.NullReferenceException"></exception>
    public void Update(TEntity entityToUpdate)
    {
        var entity = _dbSet.Find(entityToUpdate.Id);
        if (entity != null)
        {
            _context.Entry(entity).CurrentValues.SetValues(entityToUpdate);
        }

    }
}