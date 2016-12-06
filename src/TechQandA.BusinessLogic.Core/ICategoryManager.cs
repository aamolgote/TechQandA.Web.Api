using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechQandA.Models.Dto;

namespace TechQandA.BusinessLogic
{
    public interface ICategoryManager
    {
        Task<Category> AddCategory(Category category);

        Task<Category> UpdateCategory(Category category);

        Task<Category> DeleteCategory(string categoryId);

        Task<Category> GetCategory(string categoryId);

        Task<IEnumerable<Category>> GetCategories();
    }
}
