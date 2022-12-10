// ***********************************************************************
// Assembly         : DataAccessLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-03-2022
// ***********************************************************************
// <copyright file="Tag.cs" company="DataAccessLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace DataAccessLayer.Entities;

/// <summary>
/// Class Tag.
/// Implements the <see cref="DataAccessLayer.Entities.BaseEntity" />
/// </summary>
/// <seealso cref="DataAccessLayer.Entities.BaseEntity" />
public class Tag : BaseEntity
{
    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>The title.</value>
    public string Title { get; set; } = "";
    /// <summary>
    /// Gets or sets the post tags.
    /// </summary>
    /// <value>The post tags.</value>
    public ICollection<PostTag> PostTags { get; set; }
}