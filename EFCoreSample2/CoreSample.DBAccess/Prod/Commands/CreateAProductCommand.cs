using CoreSample.Domain;
using CoreSample.Infrastructure.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Prod.Commands
{
    public class CreateAProductCommand: IRequest<ProductDTO>
    {
        public ProductDTO productDTO;

        public CreateAProductCommand(ProductDTO productDTO)
        {
            this.productDTO = productDTO;
        }
    }

    public class CreateAProductCommandHandler : IRequestHandler<CreateAProductCommand, ProductDTO>
    {

        private ICoreSampleDBContext _dbContext;

        public CreateAProductCommandHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ProductDTO> Handle(CreateAProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                CurrentPrice = request.productDTO.CurrentPrice,
                Name = request.productDTO.Name,
                SKU = request.productDTO.SKU
            };

            _dbContext.DbSet<Product>().Add(product);
            await _dbContext.SaveChangesAsync();

            request.productDTO.Id = product.Id;

            return request.productDTO;
        }
    }
}
