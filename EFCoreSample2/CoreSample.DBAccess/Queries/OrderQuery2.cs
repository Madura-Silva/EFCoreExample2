using CoreSample.DBAccess.Ord.Commands;
using CoreSample.DBAccess.Ord.Queries;
using CoreSample.Infrastructure.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Queries
{
    public class OrderQuery2 : IOrderQuery
    {
        private IMediator _mediator;

        public OrderQuery2(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> DeleteAllOrdersForCustomerAsync(int id)
        {
            var query = new DeleteAllOrdersForCustomerCommand(id);
            return await _mediator.Send(query);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var query = new DeleteAnOrderCommand(id);
            return await _mediator.Send(query);
        }

        public async Task<OrderDTO> GetOrderAsync(int id)
        {
            var query = new GetOrderByOrderIdQuery(id);
            return await _mediator.Send(query);
        }

        public async Task<OrderDTO> InsertOrderAsync(OrderDTO orderDTO)
        {
            var query = new CreateOrderCommand(orderDTO);
            return await _mediator.Send(query);
        }

        public async Task<OrderDTO> UpdateOrderAsync(OrderDTO orderDTO)
        {
            var query = new UpdateOrderCommand(orderDTO);
            return await _mediator.Send(query);
        }
    }
}
