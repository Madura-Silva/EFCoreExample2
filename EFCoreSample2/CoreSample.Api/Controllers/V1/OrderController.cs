using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSample.Infrastructure.DTO;
using CoreSample.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Order
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Order/5
        [HttpGet("{id}", Name = "GetOrderById")]
        [MapToApiVersion("1.0")]
        public async Task<OrderDTO> Get(int id)
        {
            return await _orderService.GetOrderById(id);
        }

        // POST: api/Order
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Post(OrderDTO orderDTO)
        {
            //var result =Task.Run(() => _orderService.InsertOrder(orderDTO));
            //if (result.Result.Id > 0)
            //    return Ok(result);
            //else
            //    return NotFound();

            var result = await _orderService.InsertOrder(orderDTO);
            if (result.Id > 0)
                return Ok(result);
            else
                return NotFound();
        }

        // PUT: api/Order/5
        [HttpPut]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Put(OrderDTO orderDTO)
        {
            var result = await _orderService.UpdateOrder(orderDTO);
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
            return await _orderService.DeleteOrder(id);
        }
    }
}
