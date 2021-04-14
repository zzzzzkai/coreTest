using System;
using System.Collections.Generic;
using System.Text;
using Repository.IRepository;
using Service.IService;

namespace Service
{
   public class BaseService<T>:IBaseService<T> where T: class,new()

    {
        protected IBaseRepository<T> _baseRepository { get; set; }
        public IEnumerable<T> FindAll()
        {
            return _baseRepository.FindAll();
        }

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="operate"></param>
        /// <returns></returns>
        public bool Submit(Action operate)
        {

            try
            {
                _baseRepository.BeginTran();
                operate();
                _baseRepository.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                _baseRepository.RollbackTran();
                //任务出差 就不会提交 自动回滚
                throw ex;
            }

        }
    }
}
