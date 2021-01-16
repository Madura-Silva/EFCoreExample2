using CoreSample.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Cust.Commands
{
    public class DeleteACustomerCommand :IRequest<bool>
    {
        public int customerId;

        public DeleteACustomerCommand(int customerId)
        {
            this.customerId = customerId;
        }
    }

    public class DeleteACustomerCommandHandler : IRequestHandler<DeleteACustomerCommand, bool>
    {
        private ICoreSampleDBContext _dbContext;

        public DeleteACustomerCommandHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> Handle(DeleteACustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = _dbContext.DbSet<Customer>().Where(x => x.Id == request.customerId).SingleOrDefault();
                _dbContext.DbSet<Customer>().Remove(customer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
