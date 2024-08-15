using SqlSugar;

namespace AhCha.Fortunate.Entity.MSSQL
{
    [SugarTable("SysUserRole")]
    [TenantAttribute(ConstConfigId.MSSQLAhChaFortunate)]
    public class SysUserRole
    {

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long RoleId { get; set; }
    }
}
