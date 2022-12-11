// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="UserService.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using BuisnessLogicLayer.Helpers;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Models.Enums;
using BuisnessLogicLayer.Validation;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BuisnessLogicLayer.Services;

/// <summary>
/// Class UserService.
/// Implements the <see cref="IUserService" />
/// </summary>
/// <seealso cref="IUserService" />
public class UserService : IUserService
{
    /// <summary>
    /// The unit of work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;
    /// <summary>
    /// The mapper
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService" /> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="mapper">The mapper.</param>
    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all as an asynchronous operation.
    /// </summary>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        IEnumerable<User> users =  (await _unitOfWork.UserRepository.GetAllAsync());
        List<UserModel> usersModels = new List<UserModel>();

        foreach (var item in users)
            usersModels.Add(_mapper.Map<UserModel>(item));

        return usersModels;

    }

    /// <summary>
    /// Get by identifier as an asynchronous operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>A Task&lt;UserModel&gt; representing the asynchronous operation.</returns>
    /// <exception cref="BuisnessLogicLayer.Validation.PersonalBlogException">User not found</exception>
    public async Task<UserModel?> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

        if (user == null) throw new PersonalBlogException("User not found");
        
        return _mapper.Map<UserModel>(user);
    }
    /// <summary>
    /// Add as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    [Obsolete("Obsolete")]
    public async Task AddAsync(UserModel model)
    {
        model.Password = PasswordHasher.HashPassword(model.Password);
        model.UserRole = UserRole.User;
        model.Token = "";
        
        await _unitOfWork.UserRepository.AddAsync(_mapper.Map<User>(model));
        await _unitOfWork.SaveAsync();
    }



    /// <summary>
    /// Update as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task UpdateAsync(UserModel model)
    {
        _unitOfWork.UserRepository.Update(_mapper.Map<User>(model));
        await _unitOfWork.SaveAsync();

    }

    /// <summary>
    /// Delete as an asynchronous operation.
    /// </summary>
    /// <param name="modelId">The model identifier.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task DeleteAsync(int modelId)
    {
        await _unitOfWork.UserRepository.Delete(modelId);
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Get by email as an asynchronous operation.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>A Task&lt;UserModel&gt; representing the asynchronous operation.</returns>
    public async Task<UserModel?> GetByEmailAsync(string email)
    {
        User? user = (await _unitOfWork.UserRepository.GetAllAsync(u => u.Email == email)).FirstOrDefault();

        if (user is null) return null;
        
        return  _mapper.Map<UserModel>(user);
    }

    /// <summary>
    /// Check user email exist as an asynchronous operation.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>A Task&lt;System.Boolean&gt; representing the asynchronous operation.</returns>
    public async Task<bool> CheckUserEmailExistAsync(string email)
    {
        return (await _unitOfWork.UserRepository.GetAllAsync(x => x.Email == email)).Any();
    }

    /// <summary>
    /// Checks the user password and email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="password">The password.</param>
    /// <returns>System.String.</returns>
    public string CheckUserPasswordAndEmail(string email,string password)
    {
        StringBuilder sb = new StringBuilder();
        
        if (!Regex.IsMatch(email,@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
        {
            sb.Append("Email is incorrect." + Environment.NewLine);   
        }
        
        if (password.Length < 9)
        {
            sb.Append("Minimum password length should be 9." + Environment.NewLine);
        }
        if (!(Regex.IsMatch(password,"[a-z]") && Regex.IsMatch(password,"[A-Z]") 
                                            && Regex.IsMatch(password,"[0-9]")))
        {
            sb.Append("Password should be Alphanumeric." + Environment.NewLine);
        }
        if (!Regex.IsMatch(password,"[<,>,@,!,#,$,%,^,(,),:,{,},?,=,+]" ))
        {
            sb.Append("Password should contain special chars." + Environment.NewLine);   
        }

        
        return sb.ToString();
    }

    /// <summary>
    /// Creates the JWT.
    /// </summary>
    /// <param name="um">The um.</param>
    /// <returns>System.String.</returns>
    public string CreateJwt(UserModel um)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("veryverysecretkey....");
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Role, um.UserRole.ToString()),
            new Claim(ClaimTypes.Email, um.Email),
            new Claim(ClaimTypes.Name, $"{um.Name} {um.Surname}"),
            new Claim(ClaimTypes.NameIdentifier, $"{um.Id}")
        });

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.Now.AddDays(10),
            SigningCredentials = credentials
        };
        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

        return jwtTokenHandler.WriteToken(token);
    }   
}