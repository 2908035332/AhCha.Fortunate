using System;
using System.Linq;
using System.Text;

using SqlSugar;

namespace AhCha.Fortunate.Entity.MySQL
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("api_exception_log")]
    [TenantAttribute(ConstConfigId.MySqlAhChaFortunate)]
    public partial class ApiExceptionLog
    {
        public ApiExceptionLog()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, ColumnName = "Id")]
        public long Id { get; set; }

        /// <summary>
        /// Desc:Controller名称
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "ControllerName")]
        public string? ControllerName { get; set; }

        /// <summary>
        /// Desc:接口名称
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "ActionName")]
        public string? ActionName { get; set; }

        /// <summary>
        /// Desc:调用时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "CreateTime")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:用户ip地址
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "Ip")]
        public string? Ip { get; set; }

        /// <summary>
        /// Desc:异常记录
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "ExceptionText")]
        public string? ExceptionText { get; set; }

        /// <summary>
        /// Desc:用户id
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(ColumnName = "CreateUserId")]
        public long? CreateUserId { get; set; }

    }
}
