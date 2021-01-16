using CoreSample.Api.Filters;
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

    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2.0")]
    [Route("api/v{version:apiVersion}/customer")]
    [ApiController]
    public class Customer2Controller : ControllerBase
    {
        private ICustomerService _customerService;

        public Customer2Controller(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer
        [ApiKeyAuthentication]
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<List<CustomerDTO>> Get()
        {
            return await _customerService.GetAllCustomers();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public async Task<CustomerDTO> Get(int id)
        {
            return await _customerService.GetCustomerById(id);
            //return new CustomerDTO() { FirstName = "abcd", Id = 122, LastName = "xyz" };
        }

        // POST: api/Customer
        [HttpPost]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> Post([FromBody] CustomerDTO customerDTO)
        {
            var result = await _customerService.InsertCustomer(customerDTO);

            if (result.Id > 0)
                return Ok(result);
            else
                return NotFound();

        }

        // PUT: api/Customer/5
        [HttpPut]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> Put([FromBody] CustomerDTO customerDTO)
        {
            var result = await _customerService.UpdateCustomer(customerDTO);

            if (result.Id > 0)
                return Ok(result);
            else
                return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [MapToApiVersion("2.0")]
        public async Task<bool> Delete(int id)
        {
            return await _customerService.DeleteCustomer(id);
        }
    }
}
