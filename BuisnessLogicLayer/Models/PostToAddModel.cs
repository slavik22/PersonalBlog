// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-05-2022
//
// Last Modified By : Slava
// Last Modified On : 12-05-2022
// ***********************************************************************
// <copyright file="PostToAddModel.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Models.Enums;

namespace BuisnessLogicLayer.Models;

/// <summary>
/// Class PostToAddModel.
/// </summary>
public class PostToAddModel
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