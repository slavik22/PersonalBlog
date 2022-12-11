// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="IUserService.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces;

/// <summary>
/// Interface IUserService
/// Extends the <see cref="BuisnessLogicLayer.Interfaces.ICrud{BuisnessLogicLayer.Models.UserModel}" />
/// </summary>
/// <seealso cref="BuisnessLogicLayer.Interfaces.ICrud{BuisnessLogicLayer.Models.UserModel}" />
public interface IUserService : ICrud<UserModel>
{
    /// <summary>
    /// Gets the by email asynchronous.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>Task&lt;UserModel&gt;.</returns>
    Task<UserModel?> GetByEmailAsync(string email);
    /// <summary>
    /// Checks the user email exist asynchronous.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>Task&lt;System.Boolean&gt;.</returns>
    Task<bool> CheckUserEmailExistAsync(string email);
    /// <summary>
    /// Checks the user password and email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="password">The password.</param>
    /// <returns>System.String.</returns>
    string CheckUserPasswordAndEmail(string email, string password);

    /// <summary>
    /// Creates the JWT.
    /// </summary>
    /// <param name="um">The um.</param>
    /// <returns>System.String.</returns>
    string CreateJwt(UserModel um);
}