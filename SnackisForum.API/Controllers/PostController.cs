using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController: ControllerBase
    {
        private readonly IPostServices _postServices;

        public PostController(IPostServices postServices)
        {
            _postServices= postServices;
        }
        [HttpGet("discussion/{subCategoryId}")]
        public async Task<List<Post>> GetPostByDiscussion(int subCategoryId)
        {
            return await _postServices.GetBySubCategoryIdAsync(subCategoryId);
        }

        [HttpGet("{id}")]
        public async Task<Post?> GetById(int id)
        {
            return await _postServices.GetByIdAsync(id);
        }
    }
}
