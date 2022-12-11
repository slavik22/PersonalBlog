// ***********************************************************************
// Assembly         : DataAccessLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="User.cs" company="DataAccessLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataAccessLayer.Enums;

namespace DataAccessLayer.Entities;

/// <summary>
/// Class User.
/// Implements the <see cref="DataAccessLayer.Entities.BaseEntity" />
/// </summary>
/// <seealso cref="DataAccessLayer.Entities.BaseEntity" />
public class User : BaseEntity
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the surname.
    /// </summary>
    /// <value>The surname.</value>
    public string Surname { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the mobile.
    /// </summary>
    /// <value>The mobile.</value>
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>The email.</value>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>The password.</value>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the birth date.
    /// </summary>
    /// <value>The birth date.</value>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the user role.
    /// </summary>
    /// <value>The user role.</value>
    public UserRole UserRole { get; set; }

    /// <summary>
    /// Gets or sets the posts.
    /// </summary>
    /// <value>The posts.</value>
    public virtual ICollection<Post> Posts { get; set; }
}