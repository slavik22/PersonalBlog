using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostsController(IPostService postService,IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }
    
        [HttpGet("published")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPublished()
        {
            return Ok(await _postService.GetAllPublishedAsync());
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetAll()
        {
            return Ok(await _postService.GetAllAsync());
        }
        
        // GET: api/posts
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetById([FromRoute] int id)
        {
            return Ok(await _postService.GetByIdAsync(id));
        }
        // POST: api/posts
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PostModel value)
        {
            value.CreatedAt = DateTime.Now;

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
        
        // POST: api/posts/
        [HttpPost("{postId:int}/tags")]
        public async Task<ActionResult> AddTags([FromRoute] int postId, [FromBody] IEnumerable<TagModel> tags)
        {
            try
            { 
                await _postService.AddTagsAsync(postId, tags); 
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: api/posts/id/tags
        [HttpGet("{postId:int}/tags")]
        public async Task<ActionResult> GetTags([FromRoute] int postId)
        {
            try
            { 
                return Ok(await _postService.GetTagsAsync(postId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        
        // PUT: api/posts/1
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

        [HttpGet]
        [Route("user/{userId}/")]
        public ActionResult<IEnumerable<PostModel>> GetPostsOfUser(int userId)
        {
            return Ok(_postService.GetUserPostsAsync(userId));
        }
        
        [HttpGet]
        [Route("search/{text}/")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPostsSearch(string text)
        {
            try
            {
                return Ok(await _postService.GetPostsSearch(text));
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
    }
}
