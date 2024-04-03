using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.ProductManagement.Facades;
using ProductCatalog.ApiModel.ProductManagement.ReadModels;
using ProductCatalog.ApiModel.ProductManagement.WriteModels;
using System.Text.Json;

namespace ProductCatalog.API.Controllers.ProductManagement
{
    [Route("api/products/[action]")]
    [ApiController]
    public class ProductManagementController : ControllerBase
    {
        private readonly IProductManagementFacade _productManagementFacade;

        public ProductManagementController(IProductManagementFacade productManagementFacade)
        {
            _productManagementFacade = productManagementFacade;

        }

        [HttpGet]
        public IActionResult PingAlive()
        {
            return Ok("Ping successful! The endpoint is alive.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewOut>>> GetAllProducts(string? categoryFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            var (collection, paginationMetaData) = await _productManagementFacade.GetAllProducts(categoryFilter,pageNumber, pageSize);

            Response.Headers.Append("X-Pagination",JsonSerializer.Serialize(paginationMetaData));

            return Ok(collection);
        }

        [HttpGet(Name = "GetProductId")]
        public async Task<ActionResult<ProductViewOut>> GetById(int id)
        {
            var product = await _productManagementFacade.GetProductById(id);

            if(product is null)
            {
                return NotFound($"Product with id {id} does not exists");
            }

            return Ok(product);
        }


        [HttpPost]
        public async Task<ActionResult> PostProduct(CreateProductInput productInput)
        {
            int productId;

            try
            {
                productId = await _productManagementFacade.CreateProductFrom(productInput);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var routeValues = new { id = productId };

            var product = await _productManagementFacade.GetProductById(productId);

            return CreatedAtRoute("GetProductId", routeValues, product);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(UpdateProductInput userData)
        {
            try
            {
                await _productManagementFacade.UpdateProduct(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult> AddCategory(AddCategoryInput categoryInput)
        {
            try
            {
                await _productManagementFacade.AddCategory(categoryInput);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var routeValues = new { id = categoryInput.ProductId };

            var product = await _productManagementFacade.GetProductById(categoryInput.ProductId);

            return CreatedAtRoute("GetProductId", routeValues, product);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveCategory(RemoveCategoryInput categoryInput)
        {
            try
            {
                await _productManagementFacade.RemoveCategory(categoryInput);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var routeValues = new { id = categoryInput.ProductId };

            var product = await _productManagementFacade.GetProductById(categoryInput.ProductId);

            return CreatedAtRoute("GetProductId", routeValues, product);
        }
        
    }
}
