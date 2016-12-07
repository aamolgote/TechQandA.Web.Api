using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechQandA.Models.Dto;
using TechQandA.BusinessLogic;
using NLog;
using Microsoft.AspNetCore.Http;

namespace TechQandA.Web.Api.Controllers
{
    [Route("api/{categoryId}/[controller]")]
    public class SubCategoryController : Controller
    {
        #region Private Static members
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Private Members
        private readonly ISubCategoryManager subCategoryManager;
        #endregion

        public SubCategoryController(ISubCategoryManager catgMgr)
        {
            this.subCategoryManager = catgMgr;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
                return BadRequest();
            }
            try
            {
                var subCategories = this.subCategoryManager.GetSubCategories(categoryId).Result;
                return new ObjectResult(subCategories);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"An error occured in SubCategory Controller in method {nameof(Get)}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while fetching all SubCategories");
            }
        }

        // GET api/values/5
        [HttpGet("{subCategoryId}", Name = "GetSubCategory")]
        public IActionResult Get(string categoryId, string subCategoryId)
        {
            if (string.IsNullOrEmpty(categoryId) || string.IsNullOrEmpty(subCategoryId))
            {
                return BadRequest();
            }
            try
            {
                var subCategory = this.subCategoryManager.GetSubCategory(categoryId, subCategoryId).Result;
                if (subCategory == null)
                {
                    return NotFound();
                }
                return new ObjectResult(subCategory);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"An error occured in SubCategory Controller in method {nameof(Get)}", categoryId, subCategoryId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while fetching SubCategory resource");
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]SubCategory subCategory)
        {
            if (subCategory == null)
            {
                return BadRequest();
            }
            try
            {
                var newSubCategory = this.subCategoryManager.AddSubCategory(subCategory).Result;
                return CreatedAtRoute("GetSubCategory", new { id = newSubCategory.Id }, newSubCategory);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"An error occured in SubCategory Controller in method {nameof(Post)}", subCategory);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while creating SubCategory resource");
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put(string subCategoryId, [FromBody]SubCategory subCategory)
        {
            if (string.IsNullOrEmpty(subCategoryId) || subCategory.Id != subCategoryId)
            {
                return BadRequest();
            }
            try
            {
                var updatedSubCategory = this.subCategoryManager.UpdateSubCategory(subCategory).Result;
                return StatusCode(StatusCodes.Status200OK, updatedSubCategory);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"An error occured in SubCategory Controller in method {nameof(Put)}", subCategoryId, subCategory);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while updating SubCategory resource");
            }
        }

        // DELETE api/values/5
        [HttpDelete("{subCategoryId}")]
        public IActionResult Delete(string categoryId, string subCategoryId)
        {
            if (string.IsNullOrEmpty(categoryId) || string.IsNullOrEmpty(subCategoryId))
            {
                return BadRequest();
            }
            try
            {
                var subCategory = this.subCategoryManager.DeleteSubCategory(categoryId, subCategoryId).Result;
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"An error occured in SubCategory Controller in method {nameof(Delete)}", categoryId, subCategoryId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured while deleting SubCategory resource");
            }
        }
    }
}
