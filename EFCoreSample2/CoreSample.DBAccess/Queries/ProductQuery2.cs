using CoreSample.DBAccess.Prod.Commands;
using CoreSample.DBAccess.Prod.Queries;
using CoreSample.Infrastructure.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Queries
{
    public class ProductQuery2 : IProductQuery
    {
        private IMediator _mediator;

        public ProductQuery2(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var query = new DeleteAProductCommand(id);
            return await _mediator.Send(query);
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            var query = new GetAllProductsQuery();
            return await _mediator.Send(query);
        }

        public async Task<ProductDTO> GetProductAsync(int id)
        {
            var query = new GetProductByIdQuery(id);
            return await _mediator.Send(query);
        }

        public async Task<ProductDTO> InsertProductAsync(ProductDTO productDTO)
        {
            var query = new CreateAProductCommand(productDTO);
            return await _mediator.Send(query);
        }

        public async Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO)
        {
            var query = new UpdateAProductCommand(productDTO);
            return await _mediator.Send(query);
        }
    }
}
