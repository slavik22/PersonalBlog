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
    /// Add category
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="categoryModel"></param>
    public void AddCategory(int postId, CategoryModel categoryModel);
    /// <summary>
    /// Get post's categories
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public IEnumerable<CategoryModel> GetCategories(int postId);
}