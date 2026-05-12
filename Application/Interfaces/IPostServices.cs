using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPostServices
    {
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);
        Task<Post> GetByIdAsync(int id); // ska användas för att läsa en post eller för report
        Task<List<Post>> GetBySubCategoryIdAsync(int subCategoryId); // ska användas för att se alla post under en subcategory
    }
}
