using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Components.Pages
{
    public partial class CreatePost
    {
        [Parameter]
        public int SubCategoryId { get; set; }

        private string errorMessage = "";

        [SupplyParameterFromForm]
        private PostInput postModel { get; set; } = new();

        private async Task CreatePostAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(authState.User);

            if (user == null)
            {
                errorMessage = "Du måste vara inloggad.";
                return;
            }

            var post = new Post
            {
                Title = postModel.Title,
                Description = postModel.Description,
                UserId = user.Id,
                DatePublication = DateTime.Now,
                SubCategoryId = SubCategoryId
            };

            await PostService.AddAsync(post);
            Navigation.NavigateTo($"/subcategory/{SubCategoryId}");
        }

        public class PostInput
        {
            [Required]
            public string Title { get; set; } = "";
            [Required]
            public string Description { get; set; } = "";
        }
    }
}