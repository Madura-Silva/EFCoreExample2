using CoreSample.Domain;
using CoreSample.Infrastructure.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Ord.Commands
{
    public class CreateOrderCommand:IRequest<OrderDTO>
    {
        public OrderDTO orderDTO;

        public CreateOrderCommand(OrderDTO orderDTO)
        {
            this.orderDTO = orderDTO;
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDTO>
    {
        private ICoreSampleDBContext _dbContext;
        public CreateOrderCommandHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = new Order
            {
                OrderAddress = request.orderDTO.OrderAddress,
                OrderedDate = DateTime.Now,
                BillingAddress = request.orderDTO.BillingAddress,
                CustomerId = request.orderDTO.Customer.Id,
            };
            order.OrderNumber = "ORN-" + DateTime.Now.ToString("yyyy-mm-dd-HH-mm");
            order.OrderProducts = new List<OrderProduct>();
            foreach (var product in request.orderDTO.Products)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    ProductId = product.Id,
                    OrderId = order.Id,
                    NumberOfItems = product.NoOfItemsOrdered,
                });
            }


            _dbContext.DbSet<Order>().Add(order);
            await _dbContext.SaveChangesAsync();

            request.orderDTO.Id = order.Id;
            return request.orderDTO;
        }
    }
}
