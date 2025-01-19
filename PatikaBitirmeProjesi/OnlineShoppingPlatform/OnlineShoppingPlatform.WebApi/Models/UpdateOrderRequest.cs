using OnlineShoppingPlatform.Business.Operations.Orders.Dtos;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingPlatform.WebApi.Models
{
    public class UpdateOrderRequest
    {
        [Required]
        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }


        public List<OrderProductDto> OrderItems { get; set; }
    }
}
