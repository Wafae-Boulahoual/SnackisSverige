using Domain.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace Presentation.Components.Pages
{
    public partial class Messages
    {

        private List<Message> receivedMessages = new();

        private List<Message> sentMessages = new();

        private string? userId;

        protected override async Task OnInitializedAsync()
        {
            await LoadMessages();
        }

        private async Task LoadMessages()
        {
            var authState =
                await AuthenticationStateProvider.GetAuthenticationStateAsync();

            var user =
                await UserManager.GetUserAsync(authState.User);

            if (user == null)
                return;

            userId = user.Id;

            receivedMessages =
                await MessageService.GetByReceiverIdAsync(userId);

            sentMessages =
                await MessageService.GetBySenderIDAsync(userId);
        }

        private async Task MarkAsRead(Message message)
        {
            message.IsRead = true;

            await MessageService.UpdateAsync(message);

            await LoadMessages();
        }
    }
}