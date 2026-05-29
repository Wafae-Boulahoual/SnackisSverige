using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MessageServices : IMessageServices
    {
        private readonly IMessageRepository _messageRepository;

        public MessageServices(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task AddAsync(Message message)
        {
            await _messageRepository.AddAsync(message);
        }

        public async Task DeleteAsync(int id)
        {
            await _messageRepository.DeleteAsync(id);
        }

        public async Task<Message?> GetByIdAsync(int id)
        {
            return await _messageRepository.GetByIdAsync(id);
        }

        public async Task<List<Message>> GetByReceiverIdAsync(string receiverId)
        {
            return await _messageRepository.GetByReceiverIdAsync(receiverId);
        }

        public async Task<List<Message>> GetBySenderIDAsync(string senderID)
        {
            return await _messageRepository.GetBySenderIDAsync(senderID);
        }
    }
}
