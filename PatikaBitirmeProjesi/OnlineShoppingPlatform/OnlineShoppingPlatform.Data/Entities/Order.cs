using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Data.Entities
{
    public class Order : BaseEntity
    {
        //ürün tablom. Kullanıcı ile de ilişkili.

        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }

    public class OrderConfiguration : BaseConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            //base entityden created date türüyordu. Sütünun adını burada orderdate yapıyorum. 
            builder.Property(x => x.CreatedDate).HasColumnName("OrderDate");
            base.Configure(builder);
        }
    }
}
