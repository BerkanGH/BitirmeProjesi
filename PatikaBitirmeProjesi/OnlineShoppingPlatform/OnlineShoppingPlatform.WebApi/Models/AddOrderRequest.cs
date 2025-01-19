using OnlineShoppingPlatform.Business.Operations.Orders.Dtos;
using OnlineShoppingPlatform.Data.Entities;
using OnlineShoppingPlatform.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShoppingPlatform.WebApi.Models
{
    public class AddOrderRequest
    {
        
       
        [Required]
        public int UserId { get; set; }

        
   

        public List<OrderProductDto> OrderItems { get; set; }
    }

}

