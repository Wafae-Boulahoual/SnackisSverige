using Domain.Models;
namespace Presentation.Components.Pages
{
    public partial class AdminReports
    {
        private List<Report>? reports;

        // Hämtar alla rapporter när sidan laddas
        protected override async Task OnInitializedAsync()
        {
            reports = await ReportService.GetAllAsync();
        }

        private async Task MarkReviewed(Report report)
        {
            await ReportService.MarkAsReviewedAsync(report.Id);
            reports = await ReportService.GetAllAsync();
        }

        // Tar bort ett inlägg och markerar rapporten som granskad
        private async Task DeletePost(Report report)
        {
            if (report.PostId == null) return;
            await PostService.DeleteAsync(report.PostId.Value);
            await ReportService.MarkAsReviewedAsync(report.Id);
            reports = await ReportService.GetAllAsync();
        }

        private async Task DeleteComment(Report report)
        {
            if (report.CommentId == null)
            {
                return;
            }
            // måste ta bort alla rapporter kopplade till kommentaren innan vi raderar själva kommentaren
            var allReports = await ReportService.GetAllAsync();
            foreach (var r in allReports.Where(r => r.CommentId == report.CommentId))
            {
                await ReportService.DeleteAsync(r.Id);
            }
            await CommentService.DeleteAsync(report.CommentId.Value);
            reports = await ReportService.GetAllAsync(); // uppdatera listan efter
        }
    }
}