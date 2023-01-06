// ***********************************************************************
// Assembly         : WebAPI
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="PostsController.cs" company="WebAPI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Models.Enums;
using BuisnessLogicLayer.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Class PostsController.
    /// Implements the <see cref="ControllerBase" />
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
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
        /// The tag service
        /// </summary>
        private readonly ITagService _tagService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostsController" /> class.
        /// </summary>
        /// <param name="postService">The post service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="tagService">The tag service.</param>
        public PostsController(IPostService postService,IUserService userService, ITagService tagService)
        {
            _postService = postService;
            _userService = userService;
            _tagService = tagService;
        }
        /// <summary>
        /// Get all published posts
        /// </summary>
        /// <returns></returns>
        [HttpGet("published")]
        public ActionResult<IEnumerable<PostModel>> GetPublished()
        {
            return Ok( _postService.GetAllPublished());
        }

        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public ActionResult<IEnumerable<PostModel>> GetAll()
        {
            return Ok( _postService.GetAll());
        }

        /// <summary>
        /// Get post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<PostModel>> GetById([FromRoute] int id)
        {
            try
            {
                
                return Ok( _postService.GetById(id));
            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("category/{categoryId:int}")]
        public ActionResult<IEnumerable<PostModel>> GetByCategoryId([FromRoute] int categoryId)
        {
            try
            {
                return Ok( _postService.GetByCategoryId(categoryId));

            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);

            }
        }

        /// <summary>
        /// Add new post
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public  ActionResult Add([FromBody] PostModel value)
        {
            if (  _userService.GetById(value.UserId)?.UserRole == UserRole.Admin)
            {
                value.PostStatus = PostStatus.Published;
            }
            
            try
            {
                 _postService.Add(value);
                return Ok(value);
            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add new tag to post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="tagModel"></param>
        /// <returns></returns>
        [HttpPost("{postId:int}/tags")]
        public ActionResult AddTag([FromRoute] int postId, [FromBody] TagModel tagModel)
        {
            try
            { 
                 _tagService.AddTag(postId, tagModel); 
                return Ok();
            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get post's tags
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>

        [HttpGet("{postId:int}/tags")]
        public ActionResult GetTags([FromRoute] int postId)
        {
            try
            {
                return Ok( _tagService.GetTags(postId));

            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
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
        /// Delete post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        { 
            try{
                 _postService.Delete(id);
                return Ok();
            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete tag
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("tags/{tagId}")]
        public ActionResult DeleteTag([FromRoute]int tagId)
        { 
            try{
                 _tagService.Delete(tagId);
                return Ok();
            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get user's posts
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("user/{userId}/")]
        public ActionResult<IEnumerable<PostModel>> GetPostsOfUser(int userId)
        {
            try
            {
                return Ok(_postService.GetUserPosts(userId));

            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);

            }
        }
        /// <summary>
        /// Search posts
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("search/{text}/")]
        public ActionResult<IEnumerable<PostModel>> GetPostsSearch(string text)
        {
            return Ok(_postService.GetPostsSearch(text));
        }
    }
}
