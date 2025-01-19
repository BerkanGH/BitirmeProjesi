using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Data.Entities
{
    public class Setting : BaseEntity
    {
        // ayarlar için ekledim. 
        public bool MaintenenceMode { get; set; }
    }
}
