using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Business.DataProtection
{
    public interface IDataProtection
    {
        // şifreleme ve şifreyi açma classlarımı yazdım. 

        string Protect(string text);
        string UnProtect(string protectedText);
    }
}
