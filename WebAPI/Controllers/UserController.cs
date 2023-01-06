// ***********************************************************************
// Assembly         : WebAPI
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="UserController.cs" company="WebAPI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using BuisnessLogicLayer.Helpers;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Models.Enums;
using BuisnessLogicLayer.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Class UserController.
    /// Implements the <see cref="ControllerBase" />
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [Obsolete("Obsolete")]
        public IActionResult Authenticate([FromBody] UserModel? userModel)
        {
            if (userModel == null)
            {
                return BadRequest(new {Message = "Data incorrect"});
            }
            
            UserModel? user =  _userService.GetByEmail(userModel.Email);

            if (user == null)
            {
                return NotFound(new {Message = "User not found"});
            }

            if (!PasswordHasher.VerifyPassword(userModel.Password, user.Password))
            {
                return BadRequest(new {Message = "Password is incorrect"});
            }

            user.Token = _userService.CreateJwt(user);

            return Ok(new
            {
                Message = "Login Success", user.Token
            });
        }


        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel? userModel)
        {
            if (userModel == null)
            {
                return BadRequest(new { Message = "Data are incorrect" });
            }

            if ( _userService.CheckUserEmailExist(userModel.Email))
            {
                return BadRequest(new { Message = "User with such email already exists" });
            }

            string res = _userService.CheckUserPasswordAndEmail(userModel.Email, userModel.Password);
            if (!string.IsNullOrEmpty(res))
            {
                return BadRequest(new { Message = res });
            }
            
            _userService.Add(userModel);
            return Ok(new { Message = "User registered successfully" });
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            return Ok( _userService.GetAll());
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                 _userService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id:int}")]
        [Obsolete("Obsolete")]
        public ActionResult Update(int id, [FromBody] ChangeUserDataModel? value)
        {

            if (value == null)
            {
                return BadRequest(new { Message = "Data are incorrect" });
            }
            
            UserModel? user =  _userService.GetById(id);

            if (user == null)
            {
                return NotFound(new {Message = "User not found"});
            }
            
            
            if (!PasswordHasher.VerifyPassword(value.Password, user.Password))
            {
                return BadRequest(new {Message = "Password is incorrect"});
            }

            if (user.Email != value.Email &&  _userService.CheckUserEmailExist(value.Email))
            {
                return BadRequest(new {Message = "Email is already used"});
            }

            if (value.NewPassword != "")
            {
                string pass = _userService.CheckUserPasswordAndEmail(value.Email, value.NewPassword);

                if (!string.IsNullOrEmpty(pass))
                {
                    return BadRequest(new { Message = pass });
                }
            }

            UserModel userModel = new UserModel()
            {
                Id = id,
                Name = value.Name,
                Surname = value.Surname,
                Email = value.Email,
                Password = value.NewPassword == "" ? user.Password : PasswordHasher.HashPassword(value.NewPassword),
                BirthDate = user.BirthDate,
                UserRole = user.UserRole
            };

                
            try
            {
                 _userService.Update(userModel);
                return Ok();
            }
            catch (PersonalBlogException)
            {
                return BadRequest( new {Message ="User not found"});
            }
        }
        /// <summary>
        /// Update user to admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
         [Authorize]
         [HttpPut("{id:int}/makeAdmin")]
        public ActionResult UpdateToAdmin(int id, [FromBody] UpdateToAdminUserModel? value)
        {
            if (value == null)
            {
                return BadRequest(new { Message = "Data are incorrect" });
            }
            
            UserModel? user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound(new {Message = "User not found"});
            }

            UserModel um = new UserModel()
            {
                Id = value.Id,
                Name = value.Name,
                Surname = value.Surname,
                Email = value.Email,
                BirthDate = value.BirthDate,
                Password = user.Password,
                UserRole = UserRole.Admin
            };
            try
            {
                 _userService.Update(um);
                return Ok();
            }
            catch (PersonalBlogException)
            {
                return NotFound( new {Message = "User not found"});
            }
        }
    }
}
