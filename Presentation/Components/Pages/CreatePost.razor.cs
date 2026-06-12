using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
namespace Presentation.Components.Pages
{
    public partial class CreatePost
    {
        [Parameter] public int SubCategoryId { get; set; }
        private string title = "";
        private string description = "";
        private string errorMessage = "";

        private async Task CreatePostAsync()
        {
            // kolla så att användaren inte lämnat något fält tomt
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
            {
                errorMessage = "Fyll i alla fält.";
                return;
            }

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(authState.User);

            if (user == null)
            {
                errorMessage = "Du måste vara inloggad.";
                return;
            }

            // skapa ett nytt inlägg med info från formuläret
            var post = new Post
            {
                Title = title,
                Description = description,
                UserId = user.Id,
                DatePublication = DateTime.Now,
                SubCategoryId = SubCategoryId
            };

            await PostService.AddAsync(post);
            Navigation.NavigateTo($"/subcategory/{SubCategoryId}"); // skicka tillbaka till underkategorin
        }
    }
}