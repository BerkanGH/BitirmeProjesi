using OnlineShoppingPlatform.Business.Operations.Orders.Dtos;
using OnlineShoppingPlatform.Business.Operations.Products.Dtos;
using OnlineShoppingPlatform.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Business.Operations.Orders
{
    public interface IOrderService
    {
        //siparişlerimle yapacağım crud işlemlerini tanımlıyorum.
        Task<ServiceMessage> AddOrder(AddOrderDto order);
        Task<List<OrderDto>> GetOrders();

        Task<OrderDto> GetOrder(int id);

        Task<ServiceMessage> DeleteOrder(int id);

        Task<ServiceMessage> UpdateOrder(UpdateOrderDto order);
    }
}
