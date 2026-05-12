using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReportServices
    {
        Task AddAsync(Report report);
        Task UpdateAsync(Report report); // uppdatera när admin läser den
        Task DeleteAsync(int id);
        Task<Report> GetByIdAsync(int id); // för att kunna deleta den
        Task<List<Report>> GetAllAsync();
        Task MarkAsReviewedAsync(int id); // admin markerar rapporten som granskad
    }
}
