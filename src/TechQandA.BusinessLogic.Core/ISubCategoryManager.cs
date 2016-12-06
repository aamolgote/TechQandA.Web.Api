using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechQandA.Models.Dto;

namespace TechQandA.BusinessLogic
{
    public interface ISubCategoryManager
    {
        Task<SubCategory> AddSubCategory(SubCategory subCategory);

        Task<SubCategory> UpdateSubCategory(SubCategory subCategory);

        Task<SubCategory> DeleteSubCategory(string categoryId, string subCategoryId);

        Task<SubCategory> GetSubCategory(string categoryId, string subCategoryId);

        Task<IEnumerable<SubCategory>> GetSubCategories(string categoryId);
    }
}
