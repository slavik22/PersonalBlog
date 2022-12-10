// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-05-2022
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
    /// Gets the user posts asynchronous.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>IEnumerable&lt;PostModel&gt;.</returns>
    public IEnumerable<PostModel> GetUserPostsAsync(int userId);

    /// <summary>
    /// Gets the posts search.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>Task&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
    public Task<IEnumerable<PostModel>> GetPostsSearch(string text);
    //public Task AddTagAsync(int postId, TagModel tagModel);
    /// <summary>
    /// Gets all published asynchronous.
    /// </summary>
    /// <returns>Task&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
    public Task<IEnumerable<PostModel>> GetAllPublishedAsync();
    /// <summary>
    /// Gets the by category identifier asynchronous.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>Task&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
    public Task<IEnumerable<PostModel>> GetByCategoryIdAsync(int categoryId);

    //public Task<IEnumerable<TagModel>> GetTagsAsync(int postId);
}