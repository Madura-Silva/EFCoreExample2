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

namespace CoreSample.DBAccess.Prod.Queries
{
    public class GetProductByIdQuery: IRequest<ProductDTO>
    {
        public int productId;

        public GetProductByIdQuery(int productId)
        {
            this.productId = productId;
        }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private ICoreSampleDBContext _dbContext;

        public GetProductByIdQueryHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.DbSet<Product>().Where(x => x.Id == request.productId)
                 .Select(
                 y => new ProductDTO
                 {
                     Id = y.Id,
                     CurrentPrice = y.CurrentPrice,
                     Name = y.Name,
                     SKU = y.SKU
                 }).SingleOrDefaultAsync();
        }
    }
}
