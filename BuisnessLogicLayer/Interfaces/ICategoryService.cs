// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-05-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="ICategoryService.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces;

/// <summary>
/// Interface ICategoryService
/// Extends the <see cref="BuisnessLogicLayer.Interfaces.ICrud{BuisnessLogicLayer.Models.CategoryModel}" />
/// </summary>
/// <seealso cref="BuisnessLogicLayer.Interfaces.ICrud{BuisnessLogicLayer.Models.CategoryModel}" />
public interface ICategoryService : ICrud<CategoryModel>
{
    /// <summary>
    /// Adds the category asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="categoryModel">The category model.</param>
    /// <returns>Task.</returns>
    public Task AddCategoryAsync(int postId, CategoryModel categoryModel);
    /// <summary>
    /// Gets the categories asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <returns>Task&lt;IEnumerable&lt;CategoryModel&gt;&gt;.</returns>
    public Task<IEnumerable<CategoryModel>> GetCategoriesAsync(int postId);
}