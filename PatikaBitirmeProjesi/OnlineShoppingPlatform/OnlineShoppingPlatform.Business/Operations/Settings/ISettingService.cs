using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Business.Operations.Settings
{
    public interface ISettingService
    {
        //ayarlarla ilgili interface im
        Task Maintenence();

        bool GetMaintenenceMode();
    }
}
