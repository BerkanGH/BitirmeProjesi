using Microsoft.EntityFrameworkCore;
using OnlineShoppingPlatform.Business.Operations.Orders.Dtos;
using OnlineShoppingPlatform.Business.Types;
using OnlineShoppingPlatform.Data.Entities;
using OnlineShoppingPlatform.Data.Repositories;
using OnlineShoppingPlatform.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OnlineShoppingPlatform.Business.Operations.Orders
{
    //siparişlerim ile ilgili ana işlemleri burada yapıyorum. Transaction durumları için unitofwork u ve database işlemleri içinde repository lerimi alıyorum.
    


    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderProduct> _orderProductRepository;
        private readonly IRepository<Product> _productRepository;

        public OrderManager(IUnitOfWork unitOfWork, IRepository<Order> orderRepository, IRepository<OrderProduct> orderProductRepository, IRepository<Product> productRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _productRepository = productRepository;
        }
        public async Task<ServiceMessage> AddOrder(AddOrderDto order)
        {
            // Diğer tabloları da etkilediği için transaction kullanarak ekleme işlemini yapacağım. 

            await _unitOfWork.BeginTransaction();

            try
            {
                var newOrder = new Order
                {

                    UserId = order.UserId,
                    


                };
               
                _orderRepository.Add(newOrder);
                await _unitOfWork.SaveChangesAsync();
               
                decimal totalAmount = 0;

                foreach (var item in order.OrderItems)
                {
                    var product = _productRepository.GetById(item.ProductId);
                    if (product == null)
                    {
                        throw new Exception($"{item.ProductId} id li ürün bulunamadı");
                    }

                    if (product.StockQuantity < item.Quantity)
                    {
                        throw new Exception($"{product.Id} id li ürünün yeterli stoğu yok");
                    }
                    var orderDetail = new OrderProduct
                    {
                        OrderId = newOrder.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,

                    };

                    _orderProductRepository.Add(orderDetail);
                    totalAmount += item.Quantity * product.Price;

                    product.StockQuantity -= item.Quantity;
                    _productRepository.Update(product);

                }

                newOrder.TotalAmount = totalAmount;
                _orderRepository.Update(newOrder);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransaction();

                return new ServiceMessage
                {
                    IsSucceed = true,
                };

            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw;
            }


        }

        public async Task<ServiceMessage> DeleteOrder(int id)
        {
            var order = _orderRepository.GetById(id);

            if (order == null)
            {
                return new ServiceMessage { IsSucceed = false, Message = "Böyle bir sipariş bulunmuyor" };
            }

            try
            {
                await _unitOfWork.BeginTransaction();

                // İlgili OrderProduct kayıtlarını al
                var orderProducts = _orderProductRepository.GetAll(x => x.OrderId == id).ToList();

                foreach (var orderProduct in orderProducts)
                {
                    // Product tablosundaki stok miktarını geri güncelle
                    var product = _productRepository.GetById(orderProduct.ProductId);
                    if (product != null)
                    {
                        product.StockQuantity += orderProduct.Quantity;
                        _productRepository.Update(product);
                    }

                    // OrderProduct kaydını sil
                    _orderProductRepository.Delete(orderProduct);
                }

                // Siparişi sil
                _orderRepository.Delete(order);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();

                return new ServiceMessage { IsSucceed = true };
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Sipariş silinirken bir hata meydana geldi");
            }
        }

        public async Task<OrderDto> GetOrder(int id)
        {
            var order = await _orderRepository.GetAll(x => x.Id == id)
                .Select(x => new OrderDto
                {
                    UserId = x.UserId,
                    Id = x.Id,
                    TotalAmount = x.TotalAmount,
                    OrderItems = x.OrderProducts.Select(x => new OrderProductDto
                    {
                        //Price = x.Product.Price,
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                       // ProductName = x.Product.ProductName,
                    }).ToList()

                }).FirstOrDefaultAsync();

            return order;
        }

        public async Task<List<OrderDto>> GetOrders()
        {
            var order = await _orderRepository.GetAll()
             .Select(x => new OrderDto
             {
                 UserId = x.UserId,
                 Id = x.Id,
                 TotalAmount = x.TotalAmount,
                 OrderItems = x.OrderProducts.Select(x => new OrderProductDto
                 {
               //      Price = x.Product.Price,
                     ProductId = x.ProductId,
                     Quantity = x.Quantity,
                 //    ProductName = x.Product.ProductName,
                 }).ToList()

             }).ToListAsync();

            return order;
        }

        public async Task<ServiceMessage> UpdateOrder(UpdateOrderDto order)
        {
            var existingOrder = _orderRepository.GetById(order.Id);

            if (existingOrder == null)
            {
                return new ServiceMessage { IsSucceed = false, Message = "Böyle bir sipariş bulunmuyor" };
            }

            try
            {
                await _unitOfWork.BeginTransaction();

                // Mevcut OrderProduct kayıtlarını alıyoruz
                var existingOrderProducts = _orderProductRepository.GetAll(x => x.OrderId == order.Id).ToList();

                // Stokları geri yüklüyoruz
                foreach (var existingOrderProduct in existingOrderProducts)
                {
                    var product = _productRepository.GetById(existingOrderProduct.ProductId);
                    if (product != null)
                    {
                        product.StockQuantity += existingOrderProduct.Quantity;
                        _productRepository.Update(product);
                    }
                }

                // Mevcut OrderProduct kayıtlarını sil
                foreach (var orderProduct in existingOrderProducts)
                {
                    _orderProductRepository.Delete(orderProduct, false);
                }

                decimal totalAmount = 0;

                // Yeni OrderProduct kayıtlarını ekle ve stokları güncelle
                foreach (var item in order.OrderItems)
                {
                    var product = _productRepository.GetById(item.ProductId);
                    if (product == null)
                    {
                        throw new Exception($"{item.ProductId} id'li ürün bulunamadı");
                    }

                    if (product.StockQuantity < item.Quantity)
                    {
                        throw new Exception($"{product.Id} id'li ürünün yeterli stoğu yok");
                    }

                    var newOrderProduct = new OrderProduct
                    {
                        OrderId = existingOrder.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                    };

                    _orderProductRepository.Add(newOrderProduct);

                    product.StockQuantity -= item.Quantity;
                    _productRepository.Update(product);

                    totalAmount += item.Quantity * product.Price;
                }

                // Siparişin toplam tutarını güncelledik
                existingOrder.TotalAmount = totalAmount;

                // Siparişin diğer bilgilerini güncelliyoruz (örnek: kullanıcı)
                existingOrder.UserId = order.UserId;

                _orderRepository.Update(existingOrder);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();

                return new ServiceMessage { IsSucceed = true };
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Sipariş güncellenirken bir hata meydana geldi");
            }
        
        }
    }
}