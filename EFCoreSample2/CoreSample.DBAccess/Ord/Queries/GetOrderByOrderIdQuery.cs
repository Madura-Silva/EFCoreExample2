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

namespace CoreSample.DBAccess.Ord.Queries
{
    public class GetOrderByOrderIdQuery: IRequest<OrderDTO>
    {
        public int OrderId { get; set; }
        public GetOrderByOrderIdQuery(int orderId)
        {
            OrderId = orderId;
        }

        
    }

    public class GetOrderByOrderIdQueryHandler : IRequestHandler<GetOrderByOrderIdQuery, OrderDTO>
    {

        private ICoreSampleDBContext _dbContext;

        public GetOrderByOrderIdQueryHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderDTO> Handle(GetOrderByOrderIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.DbSet<Order>().Where(x => x.Id == request.OrderId)
               .Select(x => new OrderDTO
               {
                   BillingAddress = x.BillingAddress,
                   OrderAddress = x.OrderAddress,
                   OrderedDate = x.OrderedDate,
                   OrderNumber = x.OrderNumber,
                   Id = x.Id,
                   Customer = new CustomerDTO
                   {
                       FirstName = x.Customer.FirstName,
                       Id = x.Customer.Id,
                       LastName = x.Customer.LastName
                   },
                   Products = x.OrderProducts.Select(pr => new ProductDTO
                   {
                       Id = pr.Product.Id,
                       CurrentPrice = pr.Product.CurrentPrice,
                       Name = pr.Product.Name,
                       SKU = pr.Product.SKU,
                       NoOfItemsOrdered = pr.NumberOfItems,
                   }).ToList()
               }).SingleOrDefaultAsync();
        }
    }
}
