using SqlSugar;

namespace AhCha.Fortunate.Entity.MSSQL
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("SysLoginLog")]
    [TenantAttribute(ConstConfigId.MSSQLAhChaFortunate)]
    public partial class SysLoginLog
    {
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Desc:部门ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long DeptId { get; set; }

        /// <summary>
        /// Desc:用户ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long UserId { get; set; }

        /// <summary>
        /// Desc:IP
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IP { get; set; }

        /// <summary>
        /// Desc:UA
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string UAStr { get; set; }

        /// <summary>
        /// Desc:浏览器
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Browser { get; set; }

        /// <summary>
        /// Desc:系统
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string OS { get; set; }

        /// <summary>
        /// Desc:设备
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Device { get; set; }

        /// <summary>
        /// Desc:登录时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LoginTime { get; set; }

    }
}
