using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReportServices : IReportServices
    {
        private readonly IReportRepository _reportRepository;

        public ReportServices(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task AddAsync(Report report)
        {
            await _reportRepository.AddAsync(report);
        }

        public async Task DeleteAsync(int id)
        {
           await _reportRepository.DeleteAsync(id);
        }

        public async Task<List<Report>> GetAllAsync()
        {
            return await _reportRepository.GetAllAsync();
        }

        public async Task<Report> GetByIdAsync(int id)
        {
            return await _reportRepository.GetByIdAsync(id);
        }

        public async Task MarkAsReviewedAsync(int id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report != null)
            {
                report.IsReviewed = true;
                await _reportRepository.UpdateAsync(report);
            }
        }

        public async Task UpdateAsync(Report report)
        {
            await _reportRepository.UpdateAsync(report);
        }
    }
}
