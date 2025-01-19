using OnlineShoppingPlatform.Business.DataProtection;
using OnlineShoppingPlatform.Business.Operations.User.Dtos;
using OnlineShoppingPlatform.Business.Types;
using OnlineShoppingPlatform.Data.Entities;
using OnlineShoppingPlatform.Data.Enums;
using OnlineShoppingPlatform.Data.Repositories;
using OnlineShoppingPlatform.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Business.Operations.User
{
    public class UserManager : IUserService
    {

        //giriş yapma ve kullanıcı ekleme işlemlerini bu sınıfta yapıyorum.

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<OnlineShoppingPlatform.Data.Entities.User> _userRepository;
        private readonly IDataProtection _protector;
      
        public UserManager(IUnitOfWork unitOfWork,IRepository <OnlineShoppingPlatform.Data.Entities.User> repository, IDataProtection protector)
        {
            _unitOfWork = unitOfWork;
            _userRepository = repository;
             _protector = protector;
        }
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var hasMail = _userRepository.GetAll(x=>x.Email.ToLower()== user.Email.ToLower());

            if(hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Email adresi mevcut"
                };

            }
            var userEntity = new OnlineShoppingPlatform.Data.Entities.User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = _protector.Protect(user.Password),
                BirthDate = user.BirthDate,
                UserType = UserType.Customer

            };

            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("kullanıcı kaydında hata oluştu");
            }
            return new ServiceMessage
            {
                IsSucceed = true,

            };
        }

        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            var userentity = _userRepository.Get(x => x.Email.ToLower() == user.Email.ToLower());

            if (userentity == null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "ad veya şifre hatalı"

                };
            }
            var unprotectedPassword = _protector.UnProtect(userentity.Password);

            if (unprotectedPassword == user.Password)
            {
                return new ServiceMessage<UserInfoDto>


                {
                    IsSucceed = true,
                    Data = new UserInfoDto

                    {
                        Email = userentity.Email,
                        FirstName = userentity.FirstName,
                        LastName = userentity.LastName,
                        UserType = userentity.UserType,

                    }


                };


            }

            else
            {
                return new ServiceMessage<UserInfoDto>
                {

                    IsSucceed = false,
                    Message = "ad vya şifre hatalı"
                };

            }
        
        }
    }
}
