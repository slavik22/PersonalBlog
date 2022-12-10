// ***********************************************************************
// Assembly         : WebAPI
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-09-2022
// ***********************************************************************
// <copyright file="PostsController.cs" company="WebAPI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Models.Enums;
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
        /// Initializes a new instance of the <see cref="PostsController"/> class.
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
        /// Gets the published.
        /// </summary>
        /// <returns>ActionResult&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
        [HttpGet("published")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPublished()
        {
            return Ok(await _postService.GetAllPublishedAsync());
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>ActionResult&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetAll()
        {
            return Ok(await _postService.GetAllAsync());
        }

        // GET: api/posts
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetById([FromRoute] int id)
        {
            return Ok(await _postService.GetByIdAsync(id));
        }
        /// <summary>
        /// Gets the by category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns>ActionResult&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
        [HttpGet("category/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetByCategoryId([FromRoute] int categoryId)
        {
            return Ok(await _postService.GetByCategoryIdAsync(categoryId));
        }

        // POST: api/posts
        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ActionResult.</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PostModel value)
        {
            if ( (await _userService.GetByIdAsync(value.UserId))?.UserRole == UserRole.Admin)
            {
                value.PostStatus = PostStatus.Published;
            }
            
            try
            {
                await _postService.AddAsync(value);
                return Ok(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/posts/postId/tags
        /// <summary>
        /// Adds the tag.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <param name="tagModel">The tag model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost("{postId:int}/tags")]
        public async Task<ActionResult> AddTag([FromRoute] int postId, [FromBody] TagModel tagModel)
        {
            try
            { 
                await _tagService.AddTagAsync(postId, tagModel); 
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // GET: api/posts/id/tags
        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet("{postId:int}/tags")]
        public async Task<ActionResult> GetTags([FromRoute] int postId)
        {
                return Ok(await _tagService.GetTagsAsync(postId));
        }

        // PUT: api/posts/1
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

        // DELETE: api/posts/1
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
                await _postService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/posts/tags/id
        /// <summary>
        /// Deletes the tag.
        /// </summary>
        /// <param name="tagId">The tag identifier.</param>
        /// <returns>ActionResult.</returns>
        [Authorize]
        [HttpDelete("tags/{tagId}")]
        public async Task<ActionResult> DeleteTag([FromRoute]int tagId)
        { 
            try{
                await _tagService.DeleteAsync(tagId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets the posts of user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>ActionResult&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
        [HttpGet]
        [Route("user/{userId}/")]
        public ActionResult<IEnumerable<PostModel>> GetPostsOfUser(int userId)
        {
            return Ok(_postService.GetUserPostsAsync(userId));
        }

        /// <summary>
        /// Gets the posts search.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>ActionResult&lt;IEnumerable&lt;PostModel&gt;&gt;.</returns>
        [HttpGet]
        [Route("search/{text}/")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPostsSearch(string text)
        {
            return Ok(await _postService.GetPostsSearch(text));
        }
    }
}
