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

        protected override async Task OnInitializedAsync()
        {
            subCategory = await SubCategoryService.GetByIdAsync(Id);
            posts = await PostService.GetBySubCategoryIdAsync(Id);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var authState = await AuthStateProvider.GetAuthenticationStateAsync();
                IsAdmin = authState.User.Identity?.Name == "mariorossi";
                StateHasChanged();
            }
        }

        private async Task DeletePost(int postId)
        {
            await PostService.DeleteAsync(postId);
            posts = await PostService.GetBySubCategoryIdAsync(Id);
        }
    }
}