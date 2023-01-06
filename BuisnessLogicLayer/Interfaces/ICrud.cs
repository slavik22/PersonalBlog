// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
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
    /// Get all models
    /// </summary>
    /// <returns></returns>
    IEnumerable<TModel> GetAll();

    /// <summary>
    /// Get model by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    TModel? GetById(int id);

    /// <summary>
    /// Add model
    /// </summary>
    /// <param name="model"></param>
    void Add(TModel model);

    /// <summary>
    /// Update model
    /// </summary>
    /// <param name="model"></param>
    void Update(TModel model);
    /// <summary>
    /// Delete model
    /// </summary>
    /// <param name="modelId"></param>
    void Delete(int modelId);
}
