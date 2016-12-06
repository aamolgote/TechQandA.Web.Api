using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechQandA.DataAccess;
using TechQandA.Models.Dto;

namespace TechQandA.BusinessLogic
{
    public class CategoryManager : ICategoryManager
    {
        private IRepositoryCollection<Category> categoryRepository;
        public CategoryManager(IRepositoryCollection<Category> categoryRepo)
        {
            this.categoryRepository = categoryRepo;
            if (this.categoryRepository != null)
            {
                this.categoryRepository.CreateCollectionIfNotExistsAsync().Wait();
            }
        }
        public Task<Category> AddCategory(Category category)
        {
            return this.categoryRepository.CreateAsync(category);
        }

        public Task<Category> DeleteCategory(string categoryId)
        {
            return this.categoryRepository.DeleteAsync(categoryId);
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            return this.categoryRepository.GetAsync(c => 1 == 1);
        }

        public Task<Category> GetCategory(string categoryId)
        {
            return this.categoryRepository.GetAsync(categoryId);
        }

        public Task<Category> UpdateCategory(Category category)
        {
            return this.categoryRepository.UpdateAsync(category.Id, category);
        }
    }
}
