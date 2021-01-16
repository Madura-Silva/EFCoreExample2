using CoreSample.Domain;
using CoreSample.Infrastructure.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Prod.Commands
{
    public class UpdateAProductCommand:IRequest<ProductDTO>
    {
        public ProductDTO productDTO;

        public UpdateAProductCommand(ProductDTO productDTO)
        {
            this.productDTO = productDTO;
        }
    }

    public class UpdateAProductCommandHandler : IRequestHandler<UpdateAProductCommand, ProductDTO>
    {

        private ICoreSampleDBContext _dbContext;
        public UpdateAProductCommandHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ProductDTO> Handle(UpdateAProductCommand request, CancellationToken cancellationToken)
        {
            var product = _dbContext.DbSet<Product>().SingleOrDefault(x => x.Id == request.productDTO.Id);
            if (product == null)
                throw new NullReferenceException($"Product with this id {request.productDTO.Id} is not available to update");


            product.CurrentPrice = request.productDTO.CurrentPrice;
            product.Name = request.productDTO.Name;
            product.SKU = request.productDTO.SKU;
          

            _dbContext.DbSet<Product>().Update(product);
            await _dbContext.SaveChangesAsync();

            request.productDTO.Id = product.Id;

            return request.productDTO;
        }
    }
}
