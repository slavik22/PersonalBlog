// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="IPostService.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces;

/// <summary>
/// Interface IPostService
/// Extends the <see cref="BuisnessLogicLayer.Interfaces.ICrud{BuisnessLogicLayer.Models.PostModel}" />
/// </summary>
/// <seealso cref="BuisnessLogicLayer.Interfaces.ICrud{BuisnessLogicLayer.Models.PostModel}" />
public interface IPostService : ICrud<PostModel>
{
    /// <summary>
    /// Get user's posts
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public IEnumerable<PostModel> GetUserPosts(int userId);
    /// <summary>
    /// Get posts by search
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public IEnumerable<PostModel> GetPostsSearch(string text);
    /// <summary>
    /// Get all published posts
    /// </summary>
    /// <returns></returns>
    public IEnumerable<PostModel> GetAllPublished();
    /// <summary>
    /// Get category's posts
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    public IEnumerable<PostModel>GetByCategoryId(int categoryId);
}