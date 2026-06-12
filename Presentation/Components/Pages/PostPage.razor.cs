using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
namespace Presentation.Components.Pages
{
    public partial class PostPage
    {
        [Parameter] public int Id { get; set; }
        private Post? post;
        private List<Comment>? comments;
        private string commentText = "";
        private string errorMessage = "";
        private bool reportSuccess = false;
        private bool IsAdmin;

        // hämtar inlägg och kommentarer när sidan laddas
        protected override async Task OnInitializedAsync()
        {
            post = await PostServiceApi.GetByIdAsync(Id);
            comments = await CommentService.GetByPostIdAsync(Id);
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            IsAdmin = authState.User.Identity?.Name == "mariorossi";
        }

        private async Task AddComment()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(authState.User);
            if (user == null) { errorMessage = "Du måste vara inloggad."; return; }

            var comment = new Comment
            {
                Text = commentText,
                UserId = user.Id,
                PostId = Id,
                DateAnswer = DateTime.Now
            };
            await CommentService.AddAsync(comment);
            commentText = "";
            comments = await CommentService.GetByPostIdAsync(Id); // uppdatera kommentarlistan
        }

        private async Task DeletePost()
        {
            if (post == null) return;
            await PostService.DeleteAsync(post.Id);
            Navigation.NavigateTo($"/subcategory/{post.SubCategoryId}");
        }

        private async Task DeleteComment(int commentId)
        {
            // ta bort eventuella rapporter kopplade till kommentaren först
            var reports = await ReportService.GetAllAsync();
            foreach (var report in reports.Where(r => r.CommentId == commentId))
            {
                await ReportService.DeleteAsync(report.Id);
            }
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

        // samma logik som ReportPost men för kommentarer
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
    }
}