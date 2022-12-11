// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="ChangeUserDataModel.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Models.Enums;

namespace BuisnessLogicLayer.Models;

/// <summary>
/// Class ChangeUserDataModel.
/// </summary>
public class ChangeUserDataModel
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
    public string Password { get; set; } = "";
    /// <summary>
    /// Creates new password.
    /// </summary>
    /// <value>The new password.</value>
    public string NewPassword { get; set; }= "";

    /// <summary>
    /// Gets or sets the birth date.
    /// </summary>
    /// <value>The birth date.</value>
    public DateTime BirthDate { get; set; }
}