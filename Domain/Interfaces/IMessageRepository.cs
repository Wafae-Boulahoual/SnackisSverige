using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message);
        Task DeleteAsync(int id);
        Task<Message> GetByIdAsync(int id); // om man vill detela den
        Task<List<Message>> GetBySenderIDAsync(string senderID);
        Task<List<Message>> GetByReceiverIdAsync(string receiverId);

    }
}
