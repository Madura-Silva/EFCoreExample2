using CoreSample.Domain;
using CoreSample.Infrastructure.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Cust.Commands
{
    public class CreateACustomerCommand :IRequest<CustomerDTO>
    {
        public CustomerDTO CustomerDTO { get; set; }

        public CreateACustomerCommand(CustomerDTO customerDTO)
        {
            CustomerDTO = customerDTO;
        }
    }

    public class CreateACustomerCommandHandler : IRequestHandler<CreateACustomerCommand, CustomerDTO>
    {
        private ICoreSampleDBContext _dbContext;
        public CreateACustomerCommandHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerDTO> Handle(CreateACustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                FirstName = request.CustomerDTO.FirstName,
                LastName = request.CustomerDTO.LastName,

            };
            _dbContext.DbSet<Customer>().Add(customer);
            await _dbContext.SaveChangesAsync();

            request.CustomerDTO.Id = customer.Id;
            return request.CustomerDTO;
        }
    }
}
