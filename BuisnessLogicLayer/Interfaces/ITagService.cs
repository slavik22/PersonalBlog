// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
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
    /// Add tag
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="tagModel"></param>
    public void AddTag(int postId, TagModel tagModel);

    /// <summary>
    /// Get tags
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public IEnumerable<TagModel> GetTags(int postId);
}