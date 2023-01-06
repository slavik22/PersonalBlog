// ***********************************************************************
// Assembly         : DataAccessLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
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
    /// Get all entities
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null,
        string includeProperties = "");

    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    TEntity? GetById(int id,string includeProperties = "");
    /// <summary>
    /// Add entity
    /// </summary>
    /// <param name="entity"></param>
    void Add(TEntity entity);

    /// <summary>
    /// Delete entity
    /// </summary>
    /// <param name="id"></param>
    void Delete(int id);
    /// <summary>
    /// Updates the specified entity to update.
    /// </summary>
    /// <param name="entityToUpdate">The entity to update.</param>
    void Update(TEntity entityToUpdate);
}