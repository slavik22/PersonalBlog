// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="ICommentService.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces;

/// <summary>
/// Interface ICommentService
/// Extends the <see cref="BuisnessLogicLayer.Interfaces.ICrud{BuisnessLogicLayer.Models.CommentModel}" />
/// </summary>
/// <seealso cref="BuisnessLogicLayer.Interfaces.ICrud{BuisnessLogicLayer.Models.CommentModel}" />
public interface ICommentService : ICrud<CommentModel>
{
    /// <summary>
    /// Gets the post comments.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <returns>IEnumerable&lt;CommentModel&gt;.</returns>
    public Task<IEnumerable<CommentModel>> GetPostComments(int postId);
}