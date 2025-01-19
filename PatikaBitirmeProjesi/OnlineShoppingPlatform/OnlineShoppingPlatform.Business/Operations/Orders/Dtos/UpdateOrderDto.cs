using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Business.Operations.Orders.Dtos
{
    public class UpdateOrderDto
    {

        //orderlarımı güncellemek için oluşturduğum dto
        public int Id { get; set; }

       
        public int UserId { get; set; }

        public List<OrderProductDto> OrderItems { get; set; }
    }
}
