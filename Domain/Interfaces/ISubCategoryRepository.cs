using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task AddAsync(SubCategory subCategory);
        Task UpdateAsync(SubCategory subCategory);
        Task DeleteAsync(int id);
        Task<SubCategory?> GetByIdAsync(int id); //ska användas när man trycker på en subcategory
        Task<List<SubCategory>> GetByCategoryIdAsync(int categoryId); // ska användas när man tycker på category

    }
}
