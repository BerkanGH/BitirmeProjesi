using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using OnlineShoppingPlatform.Business.Operations.Products;
using OnlineShoppingPlatform.Business.Operations.Products.Dtos;
using OnlineShoppingPlatform.WebApi.Filters;
using OnlineShoppingPlatform.WebApi.Models;

namespace OnlineShoppingPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //ürünlerle ilgili crud işlemlerini yaptığım controller. 

        private readonly IProductManager _productManager;

        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;


        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(AddProductRequest request)
        {
            var addProductDto = new AddProductDto
            {
                Price = request.Price,
                ProductName = request.ProductName,
                StockQuantity = request.StockQuantity,
            };

            var result = await _productManager.AddProduct(addProductDto);

            if (result.IsSucceed)
            {
                return Ok();
            }
            else return BadRequest(result.Message);
        }

        [HttpGet]

        public async Task<IActionResult> GetProducts()
        {

            var products = await _productManager.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProducts(int id)
        {
            var product = await _productManager.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }
            else return Ok(product);
        }

        [HttpPatch("{id}/Price")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AdJustProductPrice(int id, int change)
        {
            var result = await _productManager.AdJustProductPrice(id, change);

            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            else return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productManager.DeleteProduct(id);
           
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            else return Ok();


        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [TimeFilter]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductRequest request)
        {
            var updateProductDto = new UpdateProductDto
            {
                Id = id,
                ProductName = request.ProductName,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
            };

            var result = await _productManager.UpdateProduct(updateProductDto);

            if (!result.IsSucceed)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = "Ürün başarıyla güncellendi" });
        }








    }

}