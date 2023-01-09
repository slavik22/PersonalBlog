// ***********************************************************************
// Assembly         : DataAccessLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="IUnitOfWork.cs" company="DataAccessLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

/// <summary>
/// Interface IUnitOfWork
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the user repository.
    /// </summary>
    /// <value>The user repository.</value>
    IRepository<User> UserRepository {get;}
    /// <summary>
    /// Gets the post repository.
    /// </summary>
    /// <value>The post repository.</value>
    IRepository<Post> PostRepository { get; }
    /// <summary>
    /// Gets the comment repository.
    /// </summary>
    /// <value>The comment repository.</value>
    IRepository<Comment> CommentRepository {get;}
    /// <summary>
    /// Gets the tag repository.
    /// </summary>
    /// <value>The tag repository.</value>
    IRepository<Tag> TagRepository{get;}
    /// <summary>
    /// Gets the category repository.
    /// </summary>
    /// <value>The category repository.</value>
    IRepository<Category> CategoryRepository{get;}
    /// <summary>
    /// Saves.
    /// </summary>
    /// <returns>Task.</returns>
    Task SaveAsync();

}