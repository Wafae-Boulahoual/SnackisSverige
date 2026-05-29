using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly ICommentRepository _commentRepository;

        public CommentServices(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task AddAsync(Comment comment)
        {
            await _commentRepository.AddAsync(comment);
        }

        public async Task DeleteAsync(int id)
        {
            await _commentRepository.DeleteAsync(id);
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task<List<Comment>> GetByPostIdAsync(int id)
        {
            return await _commentRepository.GetByPostIdAsync(id);   
        }

        public async Task UpdateAsync(Comment comment)
        {
            await _commentRepository.UpdateAsync(comment);
        }
    }
}
