using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
    
        // GET: api/comments/post/{id}
        [HttpGet("post/{postId:int}")]
        public ActionResult<IEnumerable<CommentModel>> GetPostComments(int postId)
        {
            try
            {
                return Ok( _commentService.GetPostComments(postId));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // POST: api/comments
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CommentModel value)
        {
            try
            {
                await _commentService.AddAsync(value);
                return Ok(value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
