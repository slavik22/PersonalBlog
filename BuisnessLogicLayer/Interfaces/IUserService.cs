﻿using BuisnessLogicLayer.Models;

namespace BuisnessLogicLayer.Interfaces;

public interface IUserService : ICrud<UserModel>
{
    Task<UserModel> GetByEmailAsync(string email); 
    Task<bool> CheckUserEmailExistAsync(string email);
    string CheckUserPasswordStrength(string password);

    string CreateJwt(UserModel um);
}