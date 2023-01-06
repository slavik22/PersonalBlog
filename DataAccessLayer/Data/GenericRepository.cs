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
    /// Get all entities from db
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public  IEnumerable<TEntity> GetAll( Expression<Func<TEntity, bool>>? filter = null,string includeProperties = "")
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

        return query.ToList();
    }

    /// <summary>
    /// Gets entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public  TEntity? GetById(int id, string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;
        foreach (var includeProperty in includeProperties.Split( ',', StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        
        return query.FirstOrDefault(p => p.Id == id);
    }
    /// <summary>
    /// Add entity
    /// </summary>
    /// <param name="entity"></param>
    public void Add(TEntity entity)
    { 
         _dbSet.Add(entity);
    }
    /// <summary>
    /// Delete entity
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Delete(int id)
    {
        TEntity entityToDelete = _dbSet.Find(id) ?? throw new InvalidOperationException();

        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
         _dbSet.Remove(entityToDelete);
    }
    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entityToUpdate"></param>
    public void Update(TEntity entityToUpdate)
    {
        var entity = _dbSet.Find(entityToUpdate.Id);
        if (entity != null)
        {
            _context.Entry(entity).CurrentValues.SetValues(entityToUpdate);
        }

    }
}