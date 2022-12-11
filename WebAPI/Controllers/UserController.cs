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
        /// Authenticates the specified user model.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("authenticate")]
        [Obsolete("Obsolete")]
        public async Task<IActionResult> Authenticate([FromBody] UserModel? userModel)
        {
            if (userModel == null)
            {
                return BadRequest(new {Message = "Data incorrect"});
            }
            
            UserModel? user = await _userService.GetByEmailAsync(userModel.Email);

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
        /// Registers the specified user model.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel? userModel)
        {
            if (userModel == null)
            {
                return BadRequest(new { Message = "Data are incorrect" });
            }

            if (await _userService.CheckUserEmailExistAsync(userModel.Email))
            {
                return BadRequest(new { Message = "User with such email already exists" });
            }

            string res = _userService.CheckUserPasswordAndEmail(userModel.Email, userModel.Password);
            if (!string.IsNullOrEmpty(res))
            {
                return BadRequest(new { Message = res });
            }
            
            await _userService.AddAsync(userModel);
            return Ok(new { Message = "User registered successfully" });
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>ActionResult&lt;IEnumerable&lt;UserModel&gt;&gt;.</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            return Ok(await _userService.GetAllAsync());
        }

        // DELETE: api/user/1
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/user/1
        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns>ActionResult.</returns>
        [Authorize]
        [HttpPut("{id:int}")]
        [Obsolete("Obsolete")]
        public async Task<ActionResult> Update(int id, [FromBody] ChangeUserDataModel? value)
        {

            if (value == null)
            {
                return BadRequest(new { Message = "Data are incorrect" });
            }
            
            UserModel? user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(new {Message = "User not found"});
            }
            
            
            if (!PasswordHasher.VerifyPassword(value.Password, user.Password))
            {
                return BadRequest(new {Message = "Password is incorrect"});
            }

            if (user.Email != value.Email && await _userService.CheckUserEmailExistAsync(value.Email))
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
                await _userService.UpdateAsync(userModel);
                return Ok();
            }
            catch (PersonalBlogException)
            {
                return BadRequest( new {Message ="User not found"});
            }
        }

        // [Authorize]
        /// <summary>
        /// Updates to admin.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns>ActionResult.</returns>
        [HttpPut("{id:int}/makeAdmin")]
        public async Task<ActionResult> UpdateToAdmin(int id, [FromBody] UpdateToAdminUserModel? value)
        {
            if (value == null)
            {
                return BadRequest(new { Message = "Data are incorrect" });
            }
            
            UserModel? user = await _userService.GetByIdAsync(id);

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
                await _userService.UpdateAsync(um);
                return Ok();
            }
            catch (PersonalBlogException)
            {
                return NotFound( new {Message = "User not found"});
            }
        }
    }
}
