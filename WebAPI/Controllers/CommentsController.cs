// ***********************************************************************
// Assembly         : WebAPI
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-01-2022
// ***********************************************************************
// <copyright file="CommentsController.cs" company="WebAPI">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Validation;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Class CommentsController.
    /// Implements the <see cref="ControllerBase" />
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        /// <summary>
        /// The comment service
        /// </summary>
        private readonly ICommentService _commentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsController"/> class.
        /// </summary>
        /// <param name="commentService">The comment service.</param>
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/comments/post/{id}
        /// <summary>
        /// Gets the post comments.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <returns>ActionResult&lt;IEnumerable&lt;CommentModel&gt;&gt;.</returns>
        [HttpGet("post/{postId:int}")]
        public ActionResult<IEnumerable<CommentModel>> GetPostComments(int postId)
        {
            try
            {
                return Ok( _commentService.GetPostComments(postId));

            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);
            }
        }
        // POST: api/comments
        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CommentModel value)
        {
            try
            {
                await _commentService.AddAsync(value);
                return Ok(value);
            }
            catch (PersonalBlogException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
