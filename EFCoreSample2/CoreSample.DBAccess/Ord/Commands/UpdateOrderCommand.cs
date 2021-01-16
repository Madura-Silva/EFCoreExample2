using CoreSample.Domain;
using CoreSample.Infrastructure.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Ord.Commands
{
    public class UpdateOrderCommand: IRequest<OrderDTO>
    {
        public OrderDTO orderDTO;

        public UpdateOrderCommand(OrderDTO orderDTO)
        {
            this.orderDTO = orderDTO;
        }

    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDTO>
    {

        private ICoreSampleDBContext _dbContext;
        public UpdateOrderCommandHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderDTO> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.DbSet<Order>()
                .Include(x => x.OrderProducts)
                .Where(x => x.Id == request.orderDTO.Id).SingleOrDefaultAsync();

            if (order != null)
            {
                order.OrderAddress = request.orderDTO.OrderAddress;
                order.BillingAddress = request.orderDTO.BillingAddress;
                order.OrderProducts.Clear();
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

            }


            _dbContext.DbSet<Order>().Update(order);
            await _dbContext.SaveChangesAsync();

            return request.orderDTO;
        }
    }
}
