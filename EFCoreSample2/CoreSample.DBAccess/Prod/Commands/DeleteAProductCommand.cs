using CoreSample.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Prod.Commands
{
    public class DeleteAProductCommand:IRequest<bool>
    {
        public int productId;

        public DeleteAProductCommand(int productId)
        {
            this.productId = productId;
        }
    }

    public class DeleteAProductCommandHandler : IRequestHandler<DeleteAProductCommand, bool>
    {

        private ICoreSampleDBContext _dbContext;
        public DeleteAProductCommandHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(DeleteAProductCommand request, CancellationToken cancellationToken)
        {
            var product = _dbContext.DbSet<Product>().Where(x => x.Id == request.productId).SingleOrDefault();
            if (product == null)
                throw new NullReferenceException($"Product with this id {request.productId} is not available to delete");


            _dbContext.DbSet<Product>().Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
