// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-01-2022
// ***********************************************************************
// <copyright file="ICrud.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace BuisnessLogicLayer.Interfaces;

/// <summary>
/// Interface ICrud
/// </summary>
/// <typeparam name="TModel">The type of the t model.</typeparam>
public interface ICrud<TModel> where TModel : class
{
    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <returns>Task&lt;IEnumerable&lt;TModel&gt;&gt;.</returns>
    Task<IEnumerable<TModel>> GetAllAsync();

    /// <summary>
    /// Gets the by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Task&lt;TModel&gt;.</returns>
    Task<TModel?> GetByIdAsync(int id);

    /// <summary>
    /// Adds the asynchronous.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>Task.</returns>
    Task AddAsync(TModel model);

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>Task.</returns>
    Task UpdateAsync(TModel model);

    /// <summary>
    /// Deletes the asynchronous.
    /// </summary>
    /// <param name="modelId">The model identifier.</param>
    /// <returns>Task.</returns>
    Task DeleteAsync(int modelId);
}
