// ***********************************************************************
// Assembly         : WebAPI
// Author           : Slava
// Created          : 12-05-2022
//
// Last Modified By : Slava
// Last Modified On : 12-09-2022
// ***********************************************************************
// <copyright file="CategoriesController.cs" company="WebAPI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Class CategoriesController.
    /// Implements the <see cref="ControllerBase" />
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        /// <summary>
        /// The post service
        /// </summary>
        private readonly IPostService _postService;
        /// <summary>
        /// The user service
        /// </summary>
        private readonly IUserService _userService;
        /// <summary>
        /// The category service
        /// </summary>
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="postService">The post service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="categoryService">The category service.</param>
        public CategoriesController(IPostService postService,IUserService userService, ICategoryService categoryService)
        {
            _postService = postService;
            _userService = userService;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>ActionResult&lt;IEnumerable&lt;CategoryModel&gt;&gt;.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        // GET: api/categories
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult&lt;CategoryModel&gt;.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CategoryModel>> GetById([FromRoute] int id)
        {
            return Ok(await _categoryService.GetByIdAsync(id));
        }

        // POST: api/categories/post/postId
        /// <summary>
        /// Adds the category.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <param name="categoryModel">The category model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost("post/{postId:int}/")]
        public async Task<ActionResult> AddCategory([FromRoute] int postId, [FromBody] CategoryModel categoryModel)
        {
            try
            { 
                await _categoryService.AddCategoryAsync(postId, categoryModel); 
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // GET: api/categories/post/postId
        /// <summary>
        /// Gets the post categories.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet("post/{postId:int}")]
        public async Task<ActionResult> GetPostCategories([FromRoute] int postId)
        {
                return Ok(await _categoryService.GetCategoriesAsync(postId));
        }

        // PUT: api/categories/1
        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns>ActionResult.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] PostModel value)
        {
            try
            {
                await _postService.UpdateAsync(value);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/categories/1
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        { 
            try{
                await _categoryService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       
    }
}
