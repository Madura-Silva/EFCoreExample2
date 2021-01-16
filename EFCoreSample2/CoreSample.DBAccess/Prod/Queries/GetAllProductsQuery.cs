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
    public class GetAllProductsQuery:IRequest<List<ProductDTO>>
    {

    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDTO>>
    {
        private ICoreSampleDBContext _dbContext;

        public GetAllProductsQueryHandler(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.DbSet<Product>().Select(pr => new ProductDTO { 
                CurrentPrice = pr.CurrentPrice,
                Id = pr.Id,
                Name = pr.Name,
                SKU =pr.SKU
            }).ToListAsync();
        }
    }
}
