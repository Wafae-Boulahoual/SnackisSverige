using Domain.Models;
using Microsoft.AspNetCore.Components.Authorization;
namespace Presentation.Components.Pages
{
    public partial class Messages
    {
        private List<Message> receivedMessages = new();
        private List<Message> sentMessages = new();

        // hämtar meddelandena för inloggad användare
        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(authState.User);
            if (user == null) return;
            receivedMessages = await MessageService.GetByReceiverIdAsync(user.Id);
            sentMessages = await MessageService.GetBySenderIDAsync(user.Id);
        }

        private async Task MarkAsRead(Message message)
        {
            message.IsRead = true;
            await MessageService.UpdateAsync(message);
            // uppdatera listan så att det syns direkt i UI
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(authState.User);
            if (user == null) return;
            receivedMessages = await MessageService.GetByReceiverIdAsync(user.Id);
        }
    }
}