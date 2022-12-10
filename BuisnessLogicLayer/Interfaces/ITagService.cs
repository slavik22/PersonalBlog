// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-05-2022
// ***********************************************************************
// <copyright file="ITagService.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces;

/// <summary>
/// Interface ITagService
/// Extends the <see cref="BuisnessLogicLayer.Interfaces.ICrud{BuisnessLogicLayer.Models.TagModel}" />
/// </summary>
/// <seealso cref="BuisnessLogicLayer.Interfaces.ICrud{BuisnessLogicLayer.Models.TagModel}" />
public interface ITagService : ICrud<TagModel>
{
    /// <summary>
    /// Adds the tag asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="tagModel">The tag model.</param>
    /// <returns>Task.</returns>
    public Task AddTagAsync(int postId, TagModel tagModel);
    /// <summary>
    /// Gets the tags asynchronous.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <returns>Task&lt;IEnumerable&lt;TagModel&gt;&gt;.</returns>
    public Task<IEnumerable<TagModel>> GetTagsAsync(int postId);
}