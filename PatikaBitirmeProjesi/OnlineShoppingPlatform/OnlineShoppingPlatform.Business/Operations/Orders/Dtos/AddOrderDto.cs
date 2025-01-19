using OnlineShoppingPlatform.Data.Entities;
using OnlineShoppingPlatform.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Business.Operations.Orders.Dtos
{
    public class AddOrderDto
    {
        //yeni sipariş oluştururken bu dto yu kullanıyorum. Daha düzenli olması için orderproductdto açıp orada product id ve quantity bilgilerini liste olarak alıyorum
        public int UserId { get; set; }

        public List<OrderProductDto> OrderItems { get; set; }
    }
}
