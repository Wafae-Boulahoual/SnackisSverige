using Application.Interfaces;
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
        public async Task<IActionResult> GetPostByDiscussion (int subCategoryId)
        {
            var posts = await _postServices.GetBySubCategoryIdAsync(subCategoryId);

            if (posts == null || !posts.Any())
            {
                return NotFound("Nessun post trovato per questa discussione.");
            }

            // Selezioniamo solo i dati richiesti dal prof (Contenuto, Autore, Data)
            var result = posts.Select(p => new
            {
                p.Title,
                p.Description,
                p.DatePublication,
                AuthorName = p.User != null ? $"{p.User.FirstName} {p.User.LastName}" : "Anonimo"
            });

            return Ok(result);
        }
    }
}
