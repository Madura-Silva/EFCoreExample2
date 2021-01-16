using CoreSample.Infrastructure.DTO;
using CoreSample.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreSample.Api.Controllers
{
    /// <summary>
    /// API Versioning : 
    /// https://codingfreaks.de/dotnet-core-api-versioning/
    /// https://www.meziantou.net/versioning-an-asp-net-core-api.htm
    /// </summary>

    [ApiVersion("1.0")]   
    [ApiExplorerSettings(GroupName = "v1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        // GET: api/Product
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "ProductById")]
        [MapToApiVersion("1.0")]
        public async Task<ProductDTO> Get(int id)
        {
            return await _productService.GetProductById(id);
        }

        // POST: api/Product
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Post(ProductDTO productDTO)
        {

            var result = await _productService.InsertProduct(productDTO);

            if (result.Id > 0)
                return Ok(result);
            else
                return NotFound();

        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Put(ProductDTO productDTO)
        {
            var result = await _productService.UpdateProduct(productDTO);

            if (result.Id > 0)
                return Ok(result);
            else
                return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<bool> DeleteAsync(int id)
        {
            return await _productService.DeleteProduct(id);

        }
    }
}
