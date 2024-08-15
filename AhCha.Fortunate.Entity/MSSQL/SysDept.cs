using SqlSugar;

namespace AhCha.Fortunate.Entity.MSSQL
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("SysDept")]
    [TenantAttribute(ConstConfigId.MSSQLAhChaFortunate)]
    public partial class SysDept
    {
        public SysDept()
        {


        }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Desc:父级部门ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ParentId { get; set; }

        /// <summary>
        /// Desc:部门名称
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
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:创建人id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? CreateUserId { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Desc:修改人id
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? UpdateUserId { get; set; }

    }
}
