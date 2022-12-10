// ***********************************************************************
// Assembly         : DataAccessLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-10-2022
// ***********************************************************************
// <copyright file="Category.cs" company="DataAccessLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// The Entities namespace.
/// </summary>
namespace DataAccessLayer.Entities;


/// <summary>
/// Class Category.
/// Implements the <see cref="T:DataAccessLayer.Entities.BaseEntity" />
/// </summary>
public class Category : BaseEntity
{

    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    /// <value>The title.</value>
    public string Title { get; set; } = "";

    /// <summary>
    /// Gets or sets the post categories.
    /// </summary>
    /// <value>The post categories.</value>
    public ICollection<PostCategory> PostCategories { get; set; }
}