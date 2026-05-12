using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SubCategoryServices : ISubCategoryServices
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoryServices(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task AddAsync(SubCategory subCategory)
        {
            await _subCategoryRepository.AddAsync(subCategory);
        }

        public async Task DeleteAsync(int id)
        {
            await _subCategoryRepository.DeleteAsync(id);
        }

        public async Task<List<SubCategory>> GetByCategoryIdAsync(int categoryId)
        {
            return await _subCategoryRepository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<SubCategory> GetByIdAsync(int id)
        {
            return await _subCategoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(SubCategory subCategory)
        {
            await _subCategoryRepository.UpdateAsync(subCategory);
        }
    }
}
