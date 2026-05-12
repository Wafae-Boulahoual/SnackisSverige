using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryServices
    {
        Task AddAsync(Category category); // Create
        Task UpdateAsync(Category category); // Update
        Task DeleteAsync(int id); //Delete
        Task<List<Category>> GetAllAsync(); // Read
        Task<Category> GetByIdAsync(int id); //Read
    }
}
