using CoreSample.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.Service
{
    public interface IProductService
    {

        Task<ProductDTO> GetProductById(int id);
        Task<ProductDTO> InsertProduct(ProductDTO productDTO);

        Task<ProductDTO> UpdateProduct(ProductDTO productDTO);

        Task<bool> DeleteProduct(int id);
    }
}
