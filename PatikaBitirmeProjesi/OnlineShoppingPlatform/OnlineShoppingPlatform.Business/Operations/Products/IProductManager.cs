using OnlineShoppingPlatform.Business.Operations.Products.Dtos;
using OnlineShoppingPlatform.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Business.Operations.Products
{
    public interface IProductManager
    {
        //burada interface ile ürünlerle ilgili yapacağım metodları gösteriyorum.
        Task<ServiceMessage> AddProduct(AddProductDto product);
        Task<List<ProductDto>> GetProducts();

        Task<ProductDto> GetProduct(int id);

        Task<ServiceMessage> AdJustProductPrice(int id, int change);

        Task<ServiceMessage> DeleteProduct(int id);

        Task<ServiceMessage> UpdateProduct(UpdateProductDto product);

    
    }
}
