using SqlSugar;

namespace AhCha.Fortunate.Entity.MSSQL
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("SysRoleMenu")]
    [TenantAttribute(ConstConfigId.MSSQLAhChaFortunate)]
    public partial class SysRoleMenu
    {

        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Desc:角色ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long RoleId { get; set; }

        /// <summary>
        /// Desc:菜单ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long MenuId { get; set; }

    }
}
