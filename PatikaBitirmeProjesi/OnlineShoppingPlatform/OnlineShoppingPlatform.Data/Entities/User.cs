using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShoppingPlatform.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Data.Entities
{
    public class User : BaseEntity
    {
        // kullanıcı tablom.
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        public UserType UserType { get; set; }

        public  ICollection<Order> Orders { get; set; }

    }
    public class UserConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x=>x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.LastName).IsRequired().HasMaxLength(50);
            base.Configure(builder);
        }
    }
}
