using CoreSample.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Ord.Commands
{
    public class DeleteAllOrdersForCustomerCommand: IRequest<bool>
    {
        public int customerId;

        public DeleteAllOrdersForCustomerCommand(int customerId) 
        {
            this.customerId = customerId;
        }
    }

    public class DeleteAllOrdersForCustomerHandler : IRequestHandler<DeleteAllOrdersForCustomerCommand, bool>
    {
        private readonly ICoreSampleDBContext _dbContext;

        public DeleteAllOrdersForCustomerHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteAllOrdersForCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = _dbContext.DbSet<Order>().Where(x => x.CustomerId == request.customerId);
                _dbContext.DbSet<Order>().RemoveRange(orders);
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
