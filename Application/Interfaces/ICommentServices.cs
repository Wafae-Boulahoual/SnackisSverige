using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICommentServices
    {
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int id);
        Task<List<Comment>> GetByPostIdAsync(int id); // för att se alla komentarer under en post
        Task<Comment?> GetByIdAsync(int id); // för eventuell report
    }
}
