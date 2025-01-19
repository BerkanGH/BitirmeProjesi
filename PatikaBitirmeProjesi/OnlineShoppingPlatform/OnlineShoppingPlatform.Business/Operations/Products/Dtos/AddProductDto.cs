using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Business.Operations.Products.Dtos
{
    public class AddProductDto
    {
       // Ürünlerle ilgili dtolarım.
        public string ProductName { get; set; }

        
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
    }
}
