using CoreSample.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Queries
{
    public interface IProductQuery
    {
        Task<ProductDTO> GetProductAsync(int id);
        Task<ProductDTO> InsertProductAsync(ProductDTO productDTO);

        Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO);

        Task<bool> DeleteProductAsync(int id);

        Task<List<ProductDTO>> GetAllProductsAsync();
    }
}
