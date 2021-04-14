using System;
using System.Linq;
using AppSetting;
using Models;
using SqlSugar;

namespace DbContext
{
    public class DbContext
    {
        private static readonly string connString = Appsetting.GetSectionValue("ConnectionStrings:Entities");//连接字符串

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient GetInstance()
        {
            //连接配置
            // SqlSugarClient是通过ConnectionConfig进行传参数详细参数如下

            // DbType                       数据库类型  
            // ConnectionString             连接字符串 
            // IsAutoCloseConnection        自动释放和关闭数据库连接，如果有事务事务结束时关闭，否则每次操作后关闭
            // ConfigureExternalServices    一些扩展层务的集成
            // MoreSettings                 更多设置
            // SlaveConnectionConfigs       主从设置

          SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connString,//连接字符串
                DbType = DbType.SqlServer,//数据库类型
                IsAutoCloseConnection = true,//开启自动释放模式
                InitKeyType = InitKeyType.Attribute,//从特性中读取主键
                IsShardSameThread = true,
                //SlaveConnectionConfigs = new List<SlaveConnectionConfig>() {//从连接
                //     new SlaveConnectionConfig() { HitRate=10, ConnectionString=slaveconnString }
                //} //主从数据库开放使用
            });

          db.Aop.OnLogExecuting = (sql, pars) =>
          {
              Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
              Console.WriteLine();
          };

          db.DbMaintenance.CreateDatabase();
          db.MappingTables.Add(typeof(User).Name, typeof(User).Name);
          db.CodeFirst.InitTables(typeof(User));//这样一个表就能成功创建了
          return db;
        }
    }
}
