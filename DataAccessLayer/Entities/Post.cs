// ***********************************************************************
// Assembly         : DataAccessLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="Post.cs" company="DataAccessLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataAccessLayer.Enums;

namespace DataAccessLayer.Entities;

/// <summary>
/// Class Post.
/// Implements the <see cref="T:DataAccessLayer.Entities.BaseEntity" />
/// </summary>
public class Post : BaseEntity
{

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>The title.</value>
    public string Title { get; set; } = "";
    /// <summary>
    /// Gets or sets the summary.
    /// </summary>
    /// <value>The summary.</value>
    public string Summary { get; set; } = "";
    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    /// <value>The content.</value>
    public string Content { get; set; } = "";

    /// <summary>
    /// Gets or sets the created at.
    /// </summary>
    /// <value>The created at.</value>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the updated at.
    /// </summary>
    /// <value>The updated at.</value>
    public DateTime UpdatedAt { get; set; }
    /// <summary>
    /// Gets or sets the post status.
    /// </summary>
    /// <value>The post status.</value>
    public PostStatus PostStatus { get; set; }

    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    /// <value>The user identifier.</value>
    public int UserId { get; set; }
    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    /// <value>The user.</value>
    public User User { get; set; }
    /// <summary>
    /// Gets or sets the comments.
    /// </summary>
    /// <value>The comments.</value>
    public ICollection<Comment> Comments { get; set; }

    /// <summary>
    /// Gets or sets the post tags.
    /// </summary>
    /// <value>The post tags.</value>
    public ICollection<PostTag> PostTags  { get; set; }
    /// <summary>
    /// Gets or sets the post categories.
    /// </summary>
    /// <value>The post categories.</value>
    public ICollection<PostCategory> PostCategories { get; set; }

}