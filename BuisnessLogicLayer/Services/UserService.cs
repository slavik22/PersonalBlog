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
    /// Get all users
    /// </summary>
    /// <returns></returns>
    public  IEnumerable<UserModel> GetAll()
    {
        IEnumerable<User> users =   _unitOfWork.UserRepository.GetAll();
        List<UserModel> usersModels = new List<UserModel>();

        foreach (var item in users)
            usersModels.Add(_mapper.Map<UserModel>(item));

        return usersModels;

    }
    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="PersonalBlogException"></exception>

    public UserModel? GetById(int id)
    {
        var user =  _unitOfWork.UserRepository.GetById(id);

        if (user == null) throw new PersonalBlogException("User not found");
        
        return _mapper.Map<UserModel>(user);
    }
    /// <summary>
    /// Add user
    /// </summary>
    /// <param name="model"></param>
    [Obsolete("Obsolete")]
    public void Add(UserModel model)
    {
        model.Password = PasswordHasher.HashPassword(model.Password);
        model.UserRole = UserRole.User;
        model.Token = "";
        
         _unitOfWork.UserRepository.Add(_mapper.Map<User>(model));
         _unitOfWork.SaveAsync();
    }
    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="model"></param>
    public void Update(UserModel model)
    {
        _unitOfWork.UserRepository.Update(_mapper.Map<User>(model));
         _unitOfWork.SaveAsync();

    }
    /// <summary>
    /// Delete user
    /// </summary>
    /// <param name="modelId"></param>
    public void Delete(int modelId)
    {
         _unitOfWork.UserRepository.Delete(modelId);
         _unitOfWork.SaveAsync();
    }
    /// <summary>
    /// Get user by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public UserModel? GetByEmail(string email)
    {
        User? user =  _unitOfWork.UserRepository.GetAll(u => u.Email == email).FirstOrDefault();

        if (user is null) return null;
        
        return  _mapper.Map<UserModel>(user);
    }

    /// <summary>
    /// Check if email is already in use
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public bool CheckUserEmailExist(string email)
    {
        return  _unitOfWork.UserRepository.GetAll(x => x.Email == email).Any();
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