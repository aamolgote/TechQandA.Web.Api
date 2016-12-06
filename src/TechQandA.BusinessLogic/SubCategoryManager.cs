using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechQandA.DataAccess;
using TechQandA.Models.Dto;

namespace TechQandA.BusinessLogic
{
    public class SubCategoryManager : ISubCategoryManager
    {
        private IRepositoryCollection<SubCategory> categoryRepository;
        public SubCategoryManager(IRepositoryCollection<SubCategory> categoryRepo)
        {
            this.categoryRepository = categoryRepo;
            if (this.categoryRepository != null)
            {
                this.categoryRepository.CreateCollectionIfNotExistsAsync().Wait();
            }
        }
        public Task<SubCategory> AddSubCategory(SubCategory category)
        {
            return this.categoryRepository.CreateAsync(category);
        }

        public Task<SubCategory> DeleteSubCategory(string categoryId, string subCategoryId)
        {
            return this.categoryRepository.DeleteAsync(subCategoryId);
        }

        public Task<IEnumerable<SubCategory>> GetSubCategories(string categoryId)
        {
            return this.categoryRepository.GetAsync(c => c.CategoryId == categoryId);
        }

        public Task<SubCategory> GetSubCategory(string categoryId, string subCategoryId)
        {
            return this.categoryRepository.GetAsync(subCategoryId);
        }

        public Task<SubCategory> UpdateSubCategory(SubCategory subCategory)
        {
            return this.categoryRepository.UpdateAsync(subCategory.Id, subCategory);
        }
    }
}
