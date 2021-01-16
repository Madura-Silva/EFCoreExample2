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

namespace CoreSample.DBAccess.Cust.Queries
{
    public class GetCustomerByIdQuery: IRequest<CustomerDTO>
    {
        public int CustomerId { get; set; }
        public GetCustomerByIdQuery(int customerId)
        {
            CustomerId = customerId;
        }

    }

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        private ICoreSampleDBContext _dbContext;

        public GetCustomerByIdQueryHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.DbSet<Customer>().Where(x => x.Id == request.CustomerId)
                .Select(x => new CustomerDTO
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id,
                    Orders = x.Orders.Select(or => new OrderDTO
                    {
                        Id = or.Id,
                        BillingAddress = or.BillingAddress,
                        OrderAddress = or.OrderAddress,
                        OrderedDate = or.OrderedDate,
                        OrderNumber = or.OrderNumber
                    }).ToList()
                }).SingleOrDefaultAsync();
        }
    }
}
