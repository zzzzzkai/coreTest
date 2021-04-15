using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Repository.IRepository;

namespace Repository.Repository
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {

    }
}
