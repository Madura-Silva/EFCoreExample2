using CoreSample.Infrastructure.DTO;
using CoreSample.Service;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
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
    [Route("api/v{version:apiVersion}/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        private ILogger _logger;

        public CustomerController(ICustomerService customerService,ILogger logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        // GET: api/Customer
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<List<CustomerDTO>> Get()
        {
            //_logger.Information("Requested all customer information 123");
            //try
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        if (i == 48)
            //        {
            //            throw new Exception("Something is wrong");
            //        }
            //        else
            //        {
            //            _logger.Information("The value of the i is {LoopCountValue}",i);
            //        }
            //    }
            //}
            //catch (System.Exception ex)
            //{

            //    _logger.Error(ex, "We cought the exception");
            //    throw ex;
            //}
            return await _customerService.GetAllCustomers();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<CustomerDTO> Get(int id)
        {
            return await _customerService.GetCustomerById(id);
            //return new CustomerDTO() { FirstName = "abcd", Id = 122, LastName = "xyz" };
        }

        // POST: api/Customer
        [HttpPost]
        [MapToApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
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
        [MapToApiVersion("1.0")]
        public async Task<bool> Delete(int id)
        {
            return await _customerService.DeleteCustomer(id);
        }
    }
}
