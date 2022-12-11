// ***********************************************************************
// Assembly         : DataAccessLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-02-2022
// ***********************************************************************
// <copyright file="IRepository.cs" company="DataAccessLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq.Expressions;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

/// <summary>
/// Interface IRepository
/// </summary>
/// <typeparam name="TEntity">The type of the t entity.</typeparam>
public interface IRepository<TEntity> where TEntity : BaseEntity


{
    
    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="orderBy">The order by.</param>
    /// <param name="includeProperties">The include properties.</param>
    /// <returns>Task&lt;IEnumerable&lt;TEntity&gt;&gt;.</returns>
     Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    /// <summary>
    /// Gets the by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="includeProperties">The include properties.</param>
    /// <returns>Task&lt;TEntity&gt;.</returns>
    Task<TEntity?> GetByIdAsync(int id,string includeProperties = "");

    /// <summary>
    /// Adds the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>Task.</returns>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Deletes the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Task.</returns>
    Task Delete(int id);
    /// <summary>
    /// Updates the specified entity to update.
    /// </summary>
    /// <param name="entityToUpdate">The entity to update.</param>
    void Update(TEntity entityToUpdate);

    public Task<TEntity?> GetByValueOneAsync(Expression<Func<TEntity, bool>> find, string includeProperties = "");
    public Task<IEnumerable<TEntity>> GetByValueAsync(Expression<Func<TEntity, bool>> find, string includeProperties = "");
}