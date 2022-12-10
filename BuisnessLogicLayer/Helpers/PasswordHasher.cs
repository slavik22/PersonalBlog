// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 11-18-2022
// ***********************************************************************
// <copyright file="PasswordHasher.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Security.Cryptography;

namespace Business.Helpers;

/// <summary>
/// Class PasswordHasher.
/// </summary>
public class PasswordHasher
{
    /// <summary>
    /// The RNG
    /// </summary>
    private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

    /// <summary>
    /// The salt size
    /// </summary>
    private static readonly int SaltSize = 16;
    /// <summary>
    /// The hash size
    /// </summary>
    private static readonly int HashSize = 20;
    /// <summary>
    /// The iterations
    /// </summary>
    private static readonly int Iterations = 10_000;

    /// <summary>
    /// Hashes the password.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <returns>System.String.</returns>
    public static string HashPassword(string password)
    {
        byte[] salt;
        rng.GetBytes(salt = new byte[SaltSize]);
        var key = new Rfc2898DeriveBytes(password, salt, Iterations);
        var hash = key.GetBytes(HashSize);

        var hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt,0,hashBytes,0,SaltSize);
        Array.Copy(hash,0,hashBytes,SaltSize,HashSize);

        var base64Hash = Convert.ToBase64String(hashBytes);

        return base64Hash;
    }

    /// <summary>
    /// Verifies the password.
    /// </summary>
    /// <param name="password">The password.</param>
    /// <param name="base6Hash">The base6 hash.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public static bool VerifyPassword(string password, string base6Hash)
    {
        var hashBytes = Convert.FromBase64String(base6Hash);

        var salt = new byte[SaltSize];
        Array.Copy(hashBytes,0,salt,0,SaltSize);

        var key = new Rfc2898DeriveBytes(password, salt, Iterations);
        byte[] hash = key.GetBytes(HashSize);

        for (int i = 0; i < HashSize; i++)
        {
            if (hashBytes[i+SaltSize] != hash[i])
            {
                return false;
            }
        }

        return true;
    }
    
}