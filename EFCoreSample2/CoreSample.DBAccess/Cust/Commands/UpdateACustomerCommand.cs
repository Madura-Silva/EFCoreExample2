using CoreSample.Domain;
using CoreSample.Infrastructure.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Cust.Commands
{
    public class UpdateACustomerCommand:IRequest<CustomerDTO>
    {
        public CustomerDTO customerDTO;

        public UpdateACustomerCommand(CustomerDTO customerDTO)
        {
            this.customerDTO = customerDTO;
        }
    }

    public class UpdateACustomerCommandHandler : IRequestHandler<UpdateACustomerCommand, CustomerDTO>
    {
        private ICoreSampleDBContext _dbContext;

        public UpdateACustomerCommandHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerDTO> Handle(UpdateACustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _dbContext.DbSet<Customer>().SingleOrDefault(x => x.Id == request.customerDTO.Id);
            if (customer == null)
                throw new NullReferenceException($"Customer with this id {request.customerDTO.Id} is not available to update");

            //var customer = new Customer
            //{
            //    Id = request.customerDTO.Id,
            //    FirstName = request.customerDTO.FirstName,
            //    LastName = request.customerDTO.LastName,

            //};

            customer.FirstName = request.customerDTO.FirstName;
            customer.LastName = request.customerDTO.LastName;

            _dbContext.DbSet<Customer>().Update(customer);
            await _dbContext.SaveChangesAsync();

            return request.customerDTO;
        }
    }


}
