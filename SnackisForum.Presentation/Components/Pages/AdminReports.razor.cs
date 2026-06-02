using Domain.Models;

namespace Presentation.Components.Pages
{
    public partial class AdminReports
    {
        private List<Report>? reports;

        protected override async Task OnInitializedAsync()
        {
            reports = await ReportService.GetAllAsync();
        }

        private async Task MarkReviewed(Report report)
        {
            await ReportService.MarkAsReviewedAsync(report.Id);
            reports = await ReportService.GetAllAsync();
        }

        private async Task DeletePost(Report report)
        {
            if (report.PostId != null)
            {
                await PostService.DeleteAsync(report.PostId.Value);
                await ReportService.MarkAsReviewedAsync(report.Id);
                reports = await ReportService.GetAllAsync();
            }
        }

        private async Task DeleteComment(Report report)
        {
            if (report.CommentId != null)
            {
                await CommentService.DeleteAsync(report.CommentId.Value);
                await ReportService.MarkAsReviewedAsync(report.Id);
                reports = await ReportService.GetAllAsync();
            }
        }
    }
}