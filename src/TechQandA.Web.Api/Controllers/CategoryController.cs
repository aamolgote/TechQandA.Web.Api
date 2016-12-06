using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechQandA.Models.Dto;
using TechQandA.BusinessLogic;

namespace TechQandA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        #region Private Members
        private readonly ICategoryManager categoryManager;
        #endregion

        public CategoryController(ICategoryManager catgMgr)
        {
            this.categoryManager = catgMgr;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return this.categoryManager.GetCategories().Result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Category Get(string categoryId)
        {
            return this.categoryManager.GetCategory(categoryId).Result;
        }

        // POST api/values
        [HttpPost]
        public Category Post([FromBody]Category category)
        {
            return this.categoryManager.AddCategory(category).Result;
        }

        // PUT api/values/5
        [HttpPut]
        public Category Put([FromBody] Category category)
        {
            return this.categoryManager.UpdateCategory(category).Result;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public Category Delete(string categoryId)
        {
            return this.categoryManager.DeleteCategory(categoryId).Result;
        }
    }
}
