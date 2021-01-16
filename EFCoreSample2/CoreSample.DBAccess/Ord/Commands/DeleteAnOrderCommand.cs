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
    public class DeleteAnOrderCommand: IRequest<bool>
    {
        public int orderId;

        public DeleteAnOrderCommand(int orderId)
        {
            this.orderId = orderId;
        }
    }


    public class DeleteAnOrderCommandHandler : IRequestHandler<DeleteAnOrderCommand, bool>
    {
        private ICoreSampleDBContext _dbContext;

        public DeleteAnOrderCommandHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteAnOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = _dbContext.DbSet<Order>().Where(x => x.Id == request.orderId).SingleOrDefault();
                _dbContext.DbSet<Order>().Remove(order);
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
