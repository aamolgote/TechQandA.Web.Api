using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechQandA.Models.Dto;
using TechQandA.BusinessLogic;
using Microsoft.AspNetCore.Http;
using NLog;

namespace TechQandA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        #region Private Static members
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion
        #region Private Members
        private readonly ICategoryManager categoryManager;
        #endregion

        public CategoryController(ICategoryManager catgMgr)
        {
            this.categoryManager = catgMgr;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var categories = this.categoryManager.GetCategories().Result;
                return new ObjectResult(categories);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"An error occured in Category Controller in method {nameof(Get)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while fetching all categories");
            }
        }

        // GET api/values/5
        [HttpGet("{categoryId}", Name = "GetCategory")]
        public IActionResult Get(string categoryId)
        {
            try
            {
                var category = this.categoryManager.GetCategory(categoryId).Result;
            if (category == null)
            {
                return NotFound();
            }
            return new ObjectResult(category);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"An error occured in Category Controller in method {nameof(Get)}", categoryId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while fetching category resource");
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            try
            {
                var newCategory = this.categoryManager.AddCategory(category).Result;
                return CreatedAtRoute("GetCategory", new { id = newCategory.Id }, newCategory);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"An error occured in Category Controller in method {nameof(Post)}", category);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while creating category resource");
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put(string categoryId, [FromBody] Category category)
        {
            if (category == null || category.Id != categoryId)
            {
                return BadRequest();
            }
            try
            {
                var updatedCategory = this.categoryManager.UpdateCategory(category).Result;
                return StatusCode(StatusCodes.Status200OK, updatedCategory);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"An error occured in Category Controller in method {nameof(Put)}", categoryId, category);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while updating category resource");
            }
        }

        // DELETE api/values/5
        [HttpDelete("{categoryId}")]
        public IActionResult Delete(string categoryId)
        {
            try
            {
                var deletedCategory = this.categoryManager.DeleteCategory(categoryId).Result;
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"An error occured in Category Controller in method {nameof(Delete)}", categoryId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while deleting category resource");
            }
        }
    }
}
