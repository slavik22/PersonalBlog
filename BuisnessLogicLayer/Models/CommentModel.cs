// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-01-2022
// ***********************************************************************
// <copyright file="CommentModel.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace BuisnessLogicLayer.Models;

/// <summary>
/// Class CommentModel.
/// </summary>
public class CommentModel
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
    /// Gets or sets the name of the author.
    /// </summary>
    /// <value>The name of the author.</value>
    public string AuthorName { get; set; } = "";
}