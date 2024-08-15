using SqlSugar;

namespace AhCha.Fortunate.Entity.MSSQL
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("SysRole")]
    [TenantAttribute(ConstConfigId.MSSQLAhChaFortunate)]
    public partial class SysRole
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
        public string Name { get; set; }

        /// <summary>
        /// Desc:部门描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Desc { get; set; }

        /// <summary>
        /// Desc:是否启用
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool? Status { get; set; }

        /// <summary>
        /// Desc:数据范围
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DataRang { get; set; }

        /// <summary>
        /// Desc:数据权限(当范围为自定义时选择的部门)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Permission { get; set; }

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
