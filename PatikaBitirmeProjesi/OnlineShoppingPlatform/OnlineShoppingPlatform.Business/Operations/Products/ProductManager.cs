using Microsoft.EntityFrameworkCore;
using OnlineShoppingPlatform.Business.Operations.Products.Dtos;
using OnlineShoppingPlatform.Business.Types;
using OnlineShoppingPlatform.Data.Entities;
using OnlineShoppingPlatform.Data.Repositories;
using OnlineShoppingPlatform.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Business.Operations.Products
{
    public class ProductManager : IProductManager
    {
        //ürün tablosu ile ilgili işlemleri yaptığım sınıfım.  

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<OrderProduct> _orderProductRepository;

        public ProductManager(IUnitOfWork unitOfWork, IRepository<Product> productRepository, IRepository<OrderProduct> orderProductRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
        }

        public async Task<ServiceMessage> AddProduct(AddProductDto product)
        {
            var hasProduct = _productRepository.GetAll(x=>x.ProductName.ToLower() == product.ProductName.ToLower()).Any();

            if(hasProduct)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "bu ürün zaten var"
                };
            }

            var productEntity = new Product
            {
                ProductName = product.ProductName,
                Price = product.Price,
                StockQuantity = product.StockQuantity,

            };
            _productRepository.Add(productEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("ürün kaydı sırasında bir hata oluştu");


        
            }

            return new ServiceMessage
            {
                IsSucceed = true,
            };
        }

        public async Task<ServiceMessage> AdJustProductPrice(int id, int change)
        {
            var product =  _productRepository.GetById(id);
            if (product == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "id ler eşleşmiyor"
                };
            }

            product.Price = change;
            _productRepository.Update(product);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("fiyat güncellenirken hata oluştu");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
            };
           
        }

        public async Task<ServiceMessage> DeleteProduct(int id)
        {
            var product =  _productRepository.GetById(id);

            if (product == null)
            {
                return new ServiceMessage { IsSucceed = false, Message = "böyle bir ürün bulunamadı" };
            }

            _productRepository.Delete(id);

            try
            {
               await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("silme işleminde hata oluştu");
            }

            return new ServiceMessage { IsSucceed = true };

        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var products = await _productRepository.GetAll(x=>x.Id==id)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    StockQuantity = x.StockQuantity,

                }).FirstOrDefaultAsync();

            return products;

        }

        public async Task<List<ProductDto>> GetProducts()
        {
            var products = await _productRepository.GetAll()
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    StockQuantity = x.StockQuantity,

                }).ToListAsync();

            return products;
        }

        public async Task<ServiceMessage> UpdateProduct(UpdateProductDto product)
        {
            var existingProduct = _productRepository.GetById(product.Id);

            if (existingProduct == null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Böyle bir ürün bulunamadı"
                };
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;

            _productRepository.Update(existingProduct);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Ürün güncellenirken bir hata oluştu");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Ürün başarıyla güncellendi"
            };
        }
    }
}
