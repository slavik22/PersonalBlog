// ***********************************************************************
// Assembly         : WebAPI
// Author           : Slava
// Created          : 12-05-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="CategoriesController.cs" company="WebAPI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Validation;
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
        /// The category service
        /// </summary>
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController" /> class.
        /// </summary>
        /// <param name="postService">The post service.</param>
        /// <param name="categoryService">The category service.</param>
        public CategoriesController(IPostService postService,ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  ActionResult<IEnumerable<CategoryModel>> GetAll()
        {
            return Ok(_categoryService.GetAll());
        }

        /// <summary>
        /// GEt category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public  ActionResult<CategoryModel> GetById([FromRoute] int id)
        {
            return Ok( _categoryService.GetById(id));
        }

        /// <summary>
        /// Add category
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="categoryModel"></param>
        /// <returns></returns>
        [HttpPost("post/{postId:int}/")]
        public  ActionResult AddCategory([FromRoute] int postId, [FromBody] CategoryModel categoryModel)
        {
            try
            { 
                 _categoryService.AddCategory(postId, categoryModel); 
                return Ok();
            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get post's categories
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>

        [HttpGet("post/{postId:int}")]
        public  ActionResult GetPostCategories([FromRoute] int postId)
        {
                return Ok( _categoryService.GetCategories(postId));
        }

        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public ActionResult Update(int id, [FromBody] PostModel value)
        {
            try
            {
                 _postService.Update(value);
                return Ok();
            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id:int}")]
        public  ActionResult Delete(int id)
        { 
            try{
                 _categoryService.Delete(id);
                return Ok();
            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);
            }
        }

       
    }
}
