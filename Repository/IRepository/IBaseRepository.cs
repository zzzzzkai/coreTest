using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repository.IRepository
{
    public interface IBaseRepository<T> where T : class, new()
    {
        #region 事务
        void RollbackTran();
        void CommitTran();
        void BeginTran();

        #endregion


        #region 查询

        IEnumerable<T> FindAll();
        T FindByClause(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindListByClause(Expression<Func<T, bool>> predicate, string orderBy = "");
        List<T> FindPageList(Expression<Func<T, bool>> predicate, string orderBy = "", int pageIndex = 1,
            int pageSize = 20);
        #endregion


        #region 插入更新

        T Insert(T t);
        bool Updata(T t);

        bool Senior(List<T> listT);

        #endregion


        #region 删除

        bool DeleteByWhere(Expression<Func<T, bool>> @where);
        bool Delete(T t);
        bool DeleteById(object id);
        bool DeleteByIds(object[] ids);

        #endregion


    }
}
