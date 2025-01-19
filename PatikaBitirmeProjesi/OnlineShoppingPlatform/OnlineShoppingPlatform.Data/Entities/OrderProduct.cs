using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineShoppingPlatform.Data.Entities
{
    public class OrderProduct : BaseEntity

    {
        //orderproduct tablom. Order ve product arasında çoka çok ilişkinin ürünü olarak ortaya çıkıyor.

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
    public class OrderProductConfiguration : BaseConfiguration<OrderProduct>
    {
        public override void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            base.Configure(builder);

            // Id'yi birincil anahtar olarak belirleme
            builder.HasKey(op => op.Id);

            // Yabancı anahtar ilişkilerinin yapılandırılması
            builder.HasOne(op => op.Order)
                   .WithMany(o => o.OrderProducts)  // 'Order' sınıfı OrderProducts koleksiyonuna sahip olmalı
                   .HasForeignKey(op => op.OrderId);

            builder.HasOne(op => op.Product)
                   .WithMany(p => p.OrderProducts)  // 'Product' sınıfı OrderProducts koleksiyonuna sahip olmalı
                   .HasForeignKey(op => op.ProductId);
        }
    }

}
