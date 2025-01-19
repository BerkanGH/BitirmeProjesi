using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingPlatform.Business.Operations.Orders;
using OnlineShoppingPlatform.Business.Operations.Orders.Dtos;
using OnlineShoppingPlatform.WebApi.Models;
using System.Security.Claims;

namespace OnlineShoppingPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // siparişlerle ilgili controllerım. Crud işlemlerini yapıyorum.


        private readonly IOrderService _orderService;


        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

          [HttpPost]

          public async Task<IActionResult> AddOrder(AddOrderRequest request)
          {
              var addOrderDto = new AddOrderDto
              {

                  UserId = request.UserId,
                  OrderItems = request.OrderItems,
              };
             var result = await _orderService.AddOrder(addOrderDto);

              if(!result.IsSucceed)
              {
                  return BadRequest(new {message = "oluşturalamadı"});
              }
              else
              {
                  return Ok( new {message = "oluşturuldu"});
              }
          }
         



        [HttpGet("{id}")]

        public async Task<IActionResult> GetOrder(int id)
        {
            var product = await _orderService.GetOrder(id);

            if (product == null)
            {
                return NotFound();
            }
            else return Ok(product);
        }

        [HttpGet]

        public async Task<IActionResult> GetOrders()
        {
            var products = await _orderService.GetOrders();

            return Ok(products);

           
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);

            if(result.IsSucceed)
            {
                return Ok();
            }

            else return BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request)

        {
            var updateOrderDto = new UpdateOrderDto
            {
                Id = id,
                UserId = id,
                OrderItems = request.OrderItems,
                
            };

            var result = await _orderService.UpdateOrder(updateOrderDto);

            if (!result.IsSucceed)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = "Sipariş güncellendi" });

        }


    }
}
