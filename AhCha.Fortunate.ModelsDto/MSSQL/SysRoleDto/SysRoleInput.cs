namespace AhCha.Fortunate.ModelsDto.MSSQL.SysRoleDto
{
    public class SysRoleInput
    {
        public long Id { get; set; }
    }


    public class QuerySysRoleInput : PageInputBase
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartQueryTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndQueryTime { get; set; }

    }

    public class AddSysRoleInput
    {
        /// <summary>
        /// Desc:主键
        /// </summary>           
        public long Id { get; set; }

        /// <summary>
        /// Desc:角色名称
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:部门描述
        /// </summary>           
        public string Desc { get; set; }

        /// <summary>
        /// Desc:是否启用
        /// </summary>           
        public bool? Status { get; set; }

        /// <summary>
        /// Desc:数据范围
        /// </summary>           
        public string DataRang { get; set; }

        /// <summary>
        /// Desc:数据权限(当范围为自定义时选择的部门)
        /// </summary>           
        public string Permission { get; set; }

    }

    public class PutSysRoleInput
    {

        /// <summary>
        /// Desc:主键
        /// </summary>           
        public long Id { get; set; }

        /// <summary>
        /// Desc:角色名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:部门描述
        /// </summary>           
        public string Desc { get; set; }

        /// <summary>
        /// Desc:是否启用
        /// </summary>           
        public bool? Status { get; set; }

        /// <summary>
        /// Desc:数据范围
        /// </summary>           
        public string DataRang { get; set; }

        /// <summary>
        /// Desc:数据权限(当范围为自定义时选择的部门)
        /// </summary>           
        public string Permission { get; set; }

    }

    public class DeleteSysRoleInput : SysRoleInput
    {


    }

    public class SettingRoleMenuInput
    {
        public long MenuId { get; set; }
        public long RoleId { get; set; }
    }

}
