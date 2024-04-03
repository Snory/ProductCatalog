using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.CategoryManagement.Facades;
using ProductCatalog.ApiModel.CategoryManagement.WriteModel;
using ProductCatalog.ApiModel.ProductManagement.ReadModels;
using System.Text.Json;

namespace ProductCatalog.API.Controllers.CategoryManagement
{
    [Route("api/category/[action]")]
    [ApiController]
    public class CategoryManagementController : ControllerBase
    {
        private readonly ICategoryManagementFacade _categoryManagementFacade;

        public CategoryManagementController(ICategoryManagementFacade categoryManagementFacade)
        {
            _categoryManagementFacade = categoryManagementFacade;
        }

        [HttpGet]
        public IActionResult PingAlive()
        {
            return Ok("Ping successful! The endpoint is alive.");
        }

        [HttpGet(Name = "GetCategoryId")]
        public async Task<ActionResult<CategoryViewOut>> GetById(int id)
        {
            var category = await _categoryManagementFacade.GetCategoryById(id);

            if (category is null)
            {
                return NotFound($"Category with id {id} does not exists");
            }

            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewOut>>> GetAll(int pageNumber = 1, int pageSize = 20)
        {
            var (collection, paginationMetaData) = await _categoryManagementFacade.GetAllCategories(pageNumber, pageSize);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            return Ok(collection);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(CreateCategoryInput categoryInput)
        {

            int categoryId;
            try
            {
                categoryId = await _categoryManagementFacade.CreateCategoryFrom(categoryInput);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var routeValues = new { id = categoryId };

            var category = await _categoryManagementFacade.GetCategoryById(categoryId);

            return CreatedAtRoute("GetCategoryId", routeValues, category);
        }
    }
}
