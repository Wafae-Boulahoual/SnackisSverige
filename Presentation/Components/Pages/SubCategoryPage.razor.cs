using Domain.Models;
using Microsoft.AspNetCore.Components;
namespace Presentation.Components.Pages
{
    public partial class SubCategoryPage
    {
        [Parameter]
        public int Id { get; set; }
        private SubCategory? subCategory;
        private List<Post>? posts;
        private bool IsAdmin;

        // hämtar subkategori och tillhörande inlägg
        protected override async Task OnInitializedAsync()
        {
            subCategory = await SubCategoryService.GetByIdAsync(Id);
            posts = await PostServiceApi.GetBySubCategoryIdAsync(Id);
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            IsAdmin = authState.User.Identity?.Name == "mariorossi";
        }

        private async Task DeletePost(int postId)
        {
            await PostService.DeleteAsync(postId);
            posts = await PostServiceApi.GetBySubCategoryIdAsync(Id); // uppdatera listan efter
        }
    }
}