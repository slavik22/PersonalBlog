// ***********************************************************************
// Assembly         : DataAccessLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="Comment.cs" company="DataAccessLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// The Entities namespace.
/// </summary>
namespace DataAccessLayer.Entities;

/// <summary>
/// Class Comment.
/// Implements the <see cref="T:DataAccessLayer.Entities.BaseEntity" />
/// </summary>
public class Comment : BaseEntity
{


    /// <summary>
    /// Gets or sets the name of the author.
    /// </summary>
    /// <value>The name of the author.</value>
    public string AuthorName { get; set; } = "";

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>The title.</value>
    public string Title { get; set; } = "";

    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    /// <value>The content.</value>
    public string Content { get; set; } = "";

    /// <summary>
    /// Gets or sets the published.
    /// </summary>
    /// <value>The published.</value>
    public DateTime Published { get; set; }

    /// <summary>
    /// Gets or sets the post identifier.
    /// </summary>
    /// <value>The post identifier.</value>
    public int PostId { get; set; }

    /// <summary>
    /// Gets or sets the post.
    /// </summary>
    /// <value>The post.</value>
    public Post Post { get; set; }
    
}