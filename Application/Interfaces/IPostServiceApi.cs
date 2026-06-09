using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPostServiceApi
    {
        Task<List<Post>> GetBySubCategoryIdAsync(int subCategoryId);
        Task<Post?> GetByIdAsync(int id);
    }
}
