using Domain.Models;
using Microsoft.AspNetCore.Components.Forms;
namespace Presentation.Components.Pages
{
    public partial class UploadPhoto
    {
        private ApplicationUser? user;
        private string statusMessage = "";

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            user = await UserManager.GetUserAsync(authState.User);
        }

        private async Task UploadProfilePhoto(InputFileChangeEventArgs e)
        {
            if (user == null) return;

            var file = e.File;

            // skapa mappen om den inte finns
            var folder = Path.Combine(WebHostEnvironment.WebRootPath, "images", "profiles");
            Directory.CreateDirectory(folder);

            // använd användarens id som filnamn så det blir unikt
            var fileName = $"{user.Id}{Path.GetExtension(file.Name)}";
            var filePath = Path.Combine(folder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024).CopyToAsync(stream);

            // spara sökvägen i databasen
            user.ImagePath = $"/images/profiles/{fileName}";
            await UserManager.UpdateAsync(user);

            statusMessage = "Profilbild uppdaterad!";
            StateHasChanged();
        }
    }
}