// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-09-2022
// ***********************************************************************
// <copyright file="PostModel.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Models.Enums;

namespace BuisnessLogicLayer.Models;

/// <summary>
/// Class PostModel.
/// </summary>
public class PostModel
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public int Id { get; set; }
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
    /// Gets or sets the name of the author.
    /// </summary>
    /// <value>The name of the author.</value>
    public string AuthorName { get; set; } = "";
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    /// <value>The user identifier.</value>
    public int UserId { get; set; }
}