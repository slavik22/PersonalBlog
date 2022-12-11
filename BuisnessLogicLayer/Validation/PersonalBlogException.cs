// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="PersonalBlogException.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace BuisnessLogicLayer.Validation;

/// <summary>
/// Class PersonalBlogException.
/// Implements the <see cref="System.Exception" />
/// </summary>
/// <seealso cref="System.Exception" />
public class PersonalBlogException: Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalBlogException" /> class.
    /// </summary>
    public PersonalBlogException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalBlogException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public PersonalBlogException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalBlogException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="innerException">The inner exception.</param>
    public PersonalBlogException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}