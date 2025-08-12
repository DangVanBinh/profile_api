using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profile_api.domain.Common;
using profile_api.domain.DTOs.Category;
using profile_api.domain.Handlers;

namespace profile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryHandler _categoryHandler;

        public CategoryController(CategoryHandler categoryHandler) 
        {
            _categoryHandler = categoryHandler;
        }
        [HttpGet]
        public async Task<Response<List<CategoryDTO>>> GetAll()
        {
            return await _categoryHandler.GetAll();
        }
    }
}
