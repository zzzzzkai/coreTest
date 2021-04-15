using System;
using System.Collections.Generic;
using System.Text;

namespace Service.IService
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> FindAll();
    }
}
