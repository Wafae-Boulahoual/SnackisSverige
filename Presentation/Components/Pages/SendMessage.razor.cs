using Domain.Models;
using Microsoft.AspNetCore.Components.Authorization;
namespace Presentation.Components.Pages
{
    public partial class SendMessage
    {
        private string receiverUserName = "";
        private string title = "";
        private string theMessage = "";
        private string errorMessage = "";
        private string successMessage = "";

        private async Task SendMessageAsync()
        {
            // kolla att alla fält är ifyllda innan vi skickar
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(theMessage) || string.IsNullOrWhiteSpace(receiverUserName))
            {
                errorMessage = "Fyll i alla fält.";
                return;
            }

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var sender = await UserManager.GetUserAsync(authState.User);
            if (sender == null) { errorMessage = "Du måste vara inloggad."; return; }

            // leta upp mottagaren med användarnamnet
            var receiver = await UserManager.FindByNameAsync(receiverUserName);
            if (receiver == null) { errorMessage = "Användaren hittades inte."; return; }

            var message = new Message
            {
                Title = title,
                TheMessage = theMessage,
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                SendingTime = DateTime.Now,
                IsRead = false
            };

            await MessageService.AddAsync(message);
            successMessage = "Meddelandet skickades!";

            // töm formuläret efter att meddelandet skickats
            receiverUserName = "";
            title = "";
            theMessage = "";
            errorMessage = "";
        }
    }
}