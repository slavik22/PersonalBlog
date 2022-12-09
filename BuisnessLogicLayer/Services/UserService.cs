using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using Business.Helpers;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Models.Enums;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BuisnessLogicLayer.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        IEnumerable<User> users =  (await _unitOfWork.UserRepository.GetAllAsync());
        List<UserModel> usersModels = new List<UserModel>();

        foreach (var item in users)
            usersModels.Add(_mapper.Map<UserModel>(item));

        return usersModels;

    }

    public async Task<UserModel> GetByIdAsync(int id)
    {
        return _mapper.Map<UserModel>(await _unitOfWork.UserRepository.GetByIdAsync(id));
    }
    public async Task AddAsync(UserModel model)
    {
        model.Password = PasswordHasher.HashPassword(model.Password);
        model.UserRole = UserRole.User;
        model.Token = "";
        
        await _unitOfWork.UserRepository.AddAsync(_mapper.Map<User>(model));
        await _unitOfWork.SaveAsync();
    }
    
    
    
    public async Task UpdateAsync(UserModel model)
    {
        _unitOfWork.UserRepository.Update(_mapper.Map<User>(model));
        await _unitOfWork.SaveAsync();

    }

    public async Task DeleteAsync(int modelId)
    {
        await _unitOfWork.UserRepository.Delete(modelId);
        await _unitOfWork.SaveAsync();
    }

    public async Task<UserModel> GetByEmailAsync(string email)
    {
        IEnumerable<User> users = await _unitOfWork.UserRepository.GetAllAsync();

        bool Expression(User us) => us.Email == email;
        
        User user = users.FirstOrDefault(Expression);
        if (user is null) return null;
        
        return  _mapper.Map<UserModel>(user);
    }

    public async Task<bool> CheckUserEmailExistAsync(string email)
    {
        IEnumerable<User> users = await _unitOfWork.UserRepository.GetAllAsync();

       return users.Any(x => x.Email == email);
    }
    
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

    public string CreateJwt(UserModel um)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("veryverysecretkey....");
        var identity = new ClaimsIdentity(new Claim[]
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