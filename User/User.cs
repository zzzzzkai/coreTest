using System.ComponentModel.DataAnnotations.Schema;
using SqlSugar;

namespace Models
{
    [Table("User")]
    // IsIdentity                自增列
    // 如果是Oracle请设置OracleSequenceName 设置后和自增一样使用
    // IsPrimaryKey              创建主键
    // ColumnName                实体类数据库列名不一样设置数据库列名
    // IsIgnore ORM              不处理该列
    // IsOnlyIgnoreInsert        插入操作时不处理该列
    // IsOnlyIgnoreUpdate        更新操作不处理该列
    // OracleSequenceName        设置Oracle序列，设置后该列等同于自增列

    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
