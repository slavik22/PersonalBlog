// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="UserModel.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Models.Enums;

namespace BuisnessLogicLayer.Models;

/// <summary>
/// Class UserModel.
/// </summary>
public class UserModel
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = "";
    /// <summary>
    /// Gets or sets the surname.
    /// </summary>
    /// <value>The surname.</value>
    public string Surname { get; set; } = "";

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>The email.</value>
    public string Email { get; set; }= "";
    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>The password.</value>

    public string Password { get; set; }= "";

    /// <summary>
    /// Gets or sets the mobile.
    /// </summary>
    /// <value>The mobile.</value>
    public string Mobile { get; set; }= "";

    /// <summary>
    /// Gets or sets the token.
    /// </summary>
    /// <value>The token.</value>
    public string Token { get; set; }= "";

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
}