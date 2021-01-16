using CoreSample.DBAccess.Queries;
using CoreSample.Domain;
using CoreSample.Infrastructure.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Cust.Queries
{
    public class GetAllCustomersQuery : IRequest<List<CustomerDTO>>
    {
    }

    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDTO>>
    {
        private ICoreSampleDBContext _dbContext;
        private ILogger _logger;

        //private ICustomerQuery _customerQuery;

        //public GetAllCustomersQueryHandler(ICustomerQuery customerQuery)
        //{
        //    _customerQuery = customerQuery;
        //}

        public GetAllCustomersQueryHandler(ICoreSampleDBContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<CustomerDTO>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            //return await _customerQuery.GetAllCustomersAsync();

            _logger.Information("Requested all Customers");

            return await _dbContext.DbSet<Customer>()
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
                }).ToListAsync();
        }
    }
}
