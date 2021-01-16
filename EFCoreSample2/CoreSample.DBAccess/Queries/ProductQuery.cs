using CoreSample.Domain;
using CoreSample.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Queries
{
    public class ProductQuery : IProductQuery
    {

        private ICoreSampleDBContext _dbContext;

        public ProductQuery(ICoreSampleDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = _dbContext.DbSet<Product>().Where(x => x.Id == id).SingleOrDefault();
                _dbContext.DbSet<Product>().Remove(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<List<ProductDTO>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDTO> GetProductAsync(int id)
        {
            return await _dbContext.DbSet<Product>().Where(x => x.Id == id)
                .Select(
                y => new ProductDTO { 
                    Id = y.Id,
                    CurrentPrice = y.CurrentPrice,
                    Name = y.Name,
                    SKU = y.SKU
                }).SingleOrDefaultAsync();
        }

        public async Task<ProductDTO> InsertProductAsync(ProductDTO productDTO)
        {

            var product = new Product
            {
                CurrentPrice = productDTO.CurrentPrice,
                Name = productDTO.Name,
                SKU = productDTO.SKU
            };

            _dbContext.DbSet<Product>().Add(product);
            await _dbContext.SaveChangesAsync();

            productDTO.Id = product.Id;

            return productDTO;
        }

        public async Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO)
        {
            var product = new Product
            {
                CurrentPrice = productDTO.CurrentPrice,
                Name = productDTO.Name,
                SKU = productDTO.SKU,
                Id = productDTO.Id
            };

            _dbContext.DbSet<Product>().Update(product);
            await _dbContext.SaveChangesAsync();

            productDTO.Id = product.Id;

            return productDTO;
        }
    }
}
