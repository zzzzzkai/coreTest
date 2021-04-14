using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Repository.IRepository;
using SqlSugar;

namespace Repository.Repository
{
   public class BaseRepository<T>:IBaseRepository<T> where T:class,new()
   {
       protected SqlSugarClient _db;

       public BaseRepository()
       {
           _db = DbContext.DbContext.GetInstance();
       }

       /// <summary>
       /// 查询全体
       /// </summary>
       /// <returns></returns>
       public IEnumerable<T> FindAll()
       {
           return _db.Queryable<T>().ToList();
       }

       /// <summary>
       /// 根据条件查找单条信息
       /// </summary>
       /// <param name="predicate"></param>
       /// <returns></returns>
       public T FindByClause(Expression<Func<T,bool>> predicate)
       {
           return _db.Queryable<T>().First(predicate);
       }

        /// <summary>
    /// 查找多条数据
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="orderBy">排序条件</param>
    /// <returns></returns>
       public IEnumerable<T> FindListByClause(Expression<Func<T, bool>> predicate , string orderBy = "")
       {
           var q =  _db.Queryable<T>().Where(predicate);
           if (!string.IsNullOrEmpty(orderBy))
           {
               q = q.OrderBy(orderBy);
           }
           return q.ToList();
       }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> FindPageList(Expression<Func<T, bool>> predicate, string orderBy = "", int pageIndex = 1, int pageSize = 20)
        {
            var totalNumber = 0;
            var totalPage = 0;
            return _db.Queryable<T>().OrderBy(orderBy).ToPageList(pageIndex, pageSize, ref totalNumber, ref totalPage);
        }

        /// <summary>
        /// 树形结构查询
        /// </summary>
        /// <param name="ParenId"></param>
        /// <param name="ChiId"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        // public List<T> FindTree(object ParenId, object ChiId, object parent)
        // {
        //     return _db.Queryable<T>().ToTree(x => x.ChiId == ChiId, x => x.ParenId == ParenId, parent);
        // }


        /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
        public T Insert(T t)
    {
        return _db.Insertable(t).ExecuteReturnEntity();
    }

        /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
        public bool Updata(T t)
    {
        return _db.Updateable(t).ExecuteCommand() > 0 ;
    }

        /// <summary>
    /// 删除实体
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
        public bool Delete(T t)
    {
        return _db.Deleteable(t).ExecuteCommand() > 0;
    }

       /// <summary>
       /// 过滤删除
       /// </summary>
       /// <param name="where"></param>
       /// <returns></returns>
        public bool DeleteByWhere(Expression<Func<T,bool>> @where)
        {
        return _db.Deleteable<T>(@where).ExecuteCommand() > 0;
        }
    
       /// <summary>
       /// 多选删除通过IDS数组
       /// </summary>
       /// <param name="ids"></param>
       /// <returns></returns>
       public bool DeleteByIds(object [] ids)
       {
           return _db.Deleteable<T>().In(ids).ExecuteCommand()>0;
       }

       /// <summary>
       /// 通过id删除
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public bool DeleteById(object id)
       {
           return _db.Deleteable<T>().In(id).ExecuteCommand() > 0;
       }

        /// <summary>
        /// 高级功能
        /// </summary>
        /// <returns></returns>
       public bool Senior(List<T> listT)
        {
            // var x =  _db.Storageable(listT).SplitInsert(x => true).SplitUpdate(x => x.Any()).ToStorage();
            var x = _db.Storageable(listT).Saveable().ToStorage();//如果数据存在则更新，不存在则插入
            x.AsInsertable.ExecuteCommand();
            x.AsUpdateable.ExecuteCommand();
            return true;
        }





        /// <summary>
        /// 通用假删除
        /// </summary>
        /// <returns></returns>
        // public bool FalseDelete(Expression<Func<T, bool>> expression)
        // {
        //     return _db.Updateable<T>()
        //         .Where(expression)
        //         .SetColumns(it => it.IsDeleted == true).ExecuteCommand() > 0;
        // }

        //事务
        public void BeginTran()
        {
            _db.Ado.BeginTran();
        }

        public void CommitTran()
        {
            _db.Ado.CommitTran();
        }

        public void RollbackTran()
        {
            _db.Ado.RollbackTran();
        }

    }
}
