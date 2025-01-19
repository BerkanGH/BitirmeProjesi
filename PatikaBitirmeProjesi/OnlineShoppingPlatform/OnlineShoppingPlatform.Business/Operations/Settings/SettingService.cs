using OnlineShoppingPlatform.Data.Entities;
using OnlineShoppingPlatform.Data.Repositories;
using OnlineShoppingPlatform.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Business.Operations.Settings
{
    public class SettingService : ISettingService
    {
        //ayarları bu sınıftan yönetiyorum. 

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Setting> _settingRepository;

        public SettingService(IUnitOfWork unitOfWork, IRepository<Setting> settingRepository)
        {
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
        }

        public bool GetMaintenenceMode()
        {
            var mainteanceMode = _settingRepository.GetById(1).MaintenenceMode;

            return mainteanceMode;
        }

        public async Task Maintenence()
        {
            var setting = _settingRepository.GetById(1);

            setting.MaintenenceMode = !setting.MaintenenceMode;

            _settingRepository.Update(setting);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Bakım sırasında bir hata ile karşılaşıldı.");
            }
        }
    }
}
