using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Components.Pages
{
    public partial class PostPage
    {
        [Parameter]
        public int Id { get; set; }

        private Post? post;
        private List<Comment>? comments;
        private string errorMessage = "";
        private bool reportSuccess = false;
        private bool IsAdmin;

        [SupplyParameterFromForm]
        private CommentInput commentModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            post = await PostService.GetByIdAsync(Id);
            comments = await CommentService.GetByPostIdAsync(Id);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                IsAdmin = authState.User.Identity?.Name == "mariorossi";
                StateHasChanged();
            }
        }

        private async Task DeletePost()
        {
            if (post == null) return;
            await PostService.DeleteAsync(post.Id);
        }

        private async Task DeleteComment(int commentId)
        {
            await CommentService.DeleteAsync(commentId);
            comments = await CommentService.GetByPostIdAsync(Id);
        }

        private async Task ReportPost(Post post)
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(authState.User);
            if (user == null) return;

            var report = new Report
            {
                Reason = "Anmält av användare",
                DateReport = DateTime.Now,
                UserId = user.Id,
                PostId = post.Id,
                IsReviewed = false
            };

            await ReportService.AddAsync(report);
            reportSuccess = true;
        }

        private async Task ReportComment(Comment comment)
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(authState.User);
            if (user == null) return;

            var report = new Report
            {
                Reason = "Anmält av användare",
                DateReport = DateTime.Now,
                UserId = user.Id,
                CommentId = comment.Id,
                IsReviewed = false
            };

            await ReportService.AddAsync(report);
            reportSuccess = true;
        }

        private async Task AddComment()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(authState.User);

            if (user == null)
            {
                errorMessage = "Du måste vara inloggad.";
                return;
            }

            var comment = new Comment
            {
                Text = commentModel.Text,
                UserId = user.Id,
                PostId = Id,
                DateAnswer = DateTime.Now
            };

            await CommentService.AddAsync(comment);
            commentModel = new();
            comments = await CommentService.GetByPostIdAsync(Id);
        }

        public class CommentInput
        {
            [Required]
            public string Text { get; set; } = "";
        }
    }
}