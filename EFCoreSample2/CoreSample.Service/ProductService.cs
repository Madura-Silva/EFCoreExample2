using CoreSample.DBAccess.Queries;
using CoreSample.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.Service
{
    public class ProductService : IProductService
    {
        private IProductQuery _productQuery;

        public ProductService(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productQuery.DeleteProductAsync(id);
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            return await _productQuery.GetProductAsync(id);
        }

        public async Task<ProductDTO> InsertProduct(ProductDTO productDTO)
        {
            return await _productQuery.InsertProductAsync(productDTO);
        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO productDTO)
        {
            return await _productQuery.UpdateProductAsync(productDTO);
        }
    }
}
