using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;

        public CategoriesController(IPostService postService,IUserService userService, ICategoryService categoryService)
        {
            _postService = postService;
            _userService = userService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }
        
        // GET: api/categories
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CategoryModel>> GetById([FromRoute] int id)
        {
            return Ok(await _categoryService.GetByIdAsync(id));
        }

        // POST: api/categories/post/postId
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
        [HttpGet("post/{postId:int}")]
        public async Task<ActionResult> GetPostCategories([FromRoute] int postId)
        {
            try
            { 
                return Ok(await _categoryService.GetCategoriesAsync(postId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/categories/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] PostModel value)
        {
            value.UpdatedAt = DateTime.Now;

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
