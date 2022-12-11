// ***********************************************************************
// Assembly         : Tests
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="EqualityComparer.cs" company="Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Tests;

using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Class UserEqualityComparer.
/// Implements the <see cref="System.Collections.Generic.IEqualityComparer{DataAccessLayer.Entities.User}" />
/// </summary>
/// <seealso cref="System.Collections.Generic.IEqualityComparer{DataAccessLayer.Entities.User}" />
internal class UserEqualityComparer : IEqualityComparer<User>
    {
    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
    /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
    /// <returns><see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals([AllowNull] User x, [AllowNull] User y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                   && x.Email == y.Email
                   && x.Mobile == y.Mobile
                   && x.Name == y.Name
                   && x.Surname == y.Surname
                   && x.BirthDate == y.BirthDate
                   && x.Password == y.Password
                ;
        }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }

/// <summary>
/// Class PostEqualityComparer.
/// Implements the <see cref="System.Collections.Generic.IEqualityComparer{DataAccessLayer.Entities.Post}" />
/// </summary>
/// <seealso cref="System.Collections.Generic.IEqualityComparer{DataAccessLayer.Entities.Post}" />
internal class PostEqualityComparer : IEqualityComparer<Post>
    {
    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
    /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
    /// <returns><see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals([AllowNull] Post x, [AllowNull] Post y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                   && x.Title == y.Title
                   && x.Summary == y.Summary
                   && x.Content == y.Content
                   && x.UpdatedAt == y.UpdatedAt
                   && x.CreatedAt == y.CreatedAt;
        }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public int GetHashCode([DisallowNull] Post obj)
        {
            return obj.GetHashCode();
        }
    }

/// <summary>
/// Class CommentEqualityComparer.
/// Implements the <see cref="System.Collections.Generic.IEqualityComparer{DataAccessLayer.Entities.Comment}" />
/// </summary>
/// <seealso cref="System.Collections.Generic.IEqualityComparer{DataAccessLayer.Entities.Comment}" />
internal class CommentEqualityComparer : IEqualityComparer<Comment>
    {
    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
    /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
    /// <returns><see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals([AllowNull] Comment x, [AllowNull] Comment y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Title == y.Title
                && x.Content== y.Content
                && x.AuthorName == y.AuthorName;
        }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public int GetHashCode([DisallowNull] Comment obj)
        {
            return obj.GetHashCode();
        }
    }

/// <summary>
/// Class CategoryEqualityComparer.
/// Implements the <see cref="System.Collections.Generic.IEqualityComparer{DataAccessLayer.Entities.Category}" />
/// </summary>
/// <seealso cref="System.Collections.Generic.IEqualityComparer{DataAccessLayer.Entities.Category}" />
internal class CategoryEqualityComparer : IEqualityComparer<Category>
    {
    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
    /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
    /// <returns><see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals([AllowNull] Category x, [AllowNull] Category y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                   && x.Title == y.Title;
        }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public int GetHashCode([DisallowNull] Category obj)
        {
            return obj.GetHashCode();
        }
    }

/// <summary>
/// Class TagEqualityComparer.
/// Implements the <see cref="System.Collections.Generic.IEqualityComparer{DataAccessLayer.Entities.Tag}" />
/// </summary>
/// <seealso cref="System.Collections.Generic.IEqualityComparer{DataAccessLayer.Entities.Tag}" />
internal class TagEqualityComparer : IEqualityComparer<Tag>
    {
    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
    /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
    /// <returns><see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals([AllowNull] Tag x, [AllowNull] Tag y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Title == y.Title;
        }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public int GetHashCode([DisallowNull] Tag obj)
        {
            return obj.GetHashCode();
        }
    }