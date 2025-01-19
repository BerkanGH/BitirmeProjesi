using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Data.Entities
{
    public class BaseEntity

    {
        // diğer tablolara miras vermesi için oluşturdum. Id gibi bölümler hepsinde ortak olduğu için daha az kod satırı kullanılması için kalıtım kullanıyorum.
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

    }

    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(x=>x.IsDeleted==false);

            builder.Property(x => x.ModifiedDate).IsRequired(false);
        }
    }
}
