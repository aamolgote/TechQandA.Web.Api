using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechQandA.Models.Dto;
using TechQandA.BusinessLogic;

namespace TechQandA.Web.Api.Controllers
{
    [Route("api/{categoryId}/[controller]")]
    public class SubCategoryController : Controller
    {
        #region Private Members
        private readonly ISubCategoryManager subCategoryManager;
        #endregion

        public SubCategoryController(ISubCategoryManager catgMgr)
        {
            this.subCategoryManager = catgMgr;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<SubCategory> Get(string categoryId)
        {
            return this.subCategoryManager.GetSubCategories(categoryId).Result;
        }

        // GET api/values/5
        [HttpGet("{subCategoryId}")]
        public SubCategory Get(string categoryId, string subCategoryId)
        {
            return this.subCategoryManager.GetSubCategory(categoryId, subCategoryId).Result;
        }

        // POST api/values
        [HttpPost]
        public SubCategory Post([FromBody]SubCategory subCategory)
        {
            return this.subCategoryManager.AddSubCategory(subCategory).Result;
        }

        // PUT api/values/5
        [HttpPut]
        public SubCategory Put([FromBody]SubCategory subCategory)
        {
            return this.subCategoryManager.UpdateSubCategory(subCategory).Result;
        }

        // DELETE api/values/5
        [HttpDelete("{subCategoryId}")]
        public SubCategory Delete(string categoryId, string subCategoryId)
        {
            return this.subCategoryManager.DeleteSubCategory(categoryId, subCategoryId).Result;
        }
    }
}
