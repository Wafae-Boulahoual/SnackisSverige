using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IReportRepository
    {
        Task AddAsync(Report report);
        Task UpdateAsync(Report report); 
        Task DeleteAsync(int id);
        Task <Report?> GetByIdAsync(int id); 
        Task<List<Report>> GetAllAsync();
    }
}
