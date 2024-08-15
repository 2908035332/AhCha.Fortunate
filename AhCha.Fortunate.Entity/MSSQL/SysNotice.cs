using SqlSugar;

namespace AhCha.Fortunate.Entity.MSSQL
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("SysNotice")]
    [TenantAttribute(ConstConfigId.MSSQLAhChaFortunate)]
    public partial class SysNotice
    {
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Desc:角色名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Content { get; set; }

        /// <summary>
        /// Desc:类型(10公告 20通知)
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Type { get; set; }

        /// <summary>
        /// Desc:是否启用
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool? Status { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? CreateUserId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? UpdateUserId { get; set; }

    }
}
