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
        private readonly ITagService _tagService;

        public PostsController(IPostService postService,IUserService userService, ITagService tagService)
        {
            _postService = postService;
            _userService = userService;
            _tagService = tagService;
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
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetById([FromRoute] int id)
        {
            return Ok(await _postService.GetByIdAsync(id));
        }
        [HttpGet("category/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetByCategoryId([FromRoute] int categoryId)
        {
            return Ok(await _postService.GetByCategoryIdAsync(categoryId));
        }
        
        // POST: api/posts
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
        [HttpGet("{postId:int}/tags")]
        public async Task<ActionResult> GetTags([FromRoute] int postId)
        {
                return Ok(await _tagService.GetTagsAsync(postId));
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

        // DELETE: api/posts/tags/id
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
            return Ok(await _postService.GetPostsSearch(text));
        }
    }
}
