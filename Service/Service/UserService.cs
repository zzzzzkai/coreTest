using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Repository.IRepository;
using Service.IService;

namespace Service
{
    public class UserService:BaseService<User>,IUserService
    {
        private readonly IUserRepository _iUserRepository;
        public UserService(IUserRepository IuserRepository)
        {
            _iUserRepository = IuserRepository;
        }

        public object getAll()
        {
            return _iUserRepository.FindAll();
        }
    }
}
