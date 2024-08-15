namespace AhCha.Fortunate.ModelsDto.MSSQL.SysRoleDto
{
    public class SysRoleOutput
    {
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

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    public class RoleTreeMenuOutput
    {
        public long? ParentId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public bool IsHide { get; set; }
        public bool IsAuth { get; set; }
        public List<RoleTreeMenuOutput> children { get; set; }
    }

    public class RoleTreeDeptOutput
    {
        public long ParentId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool IsAuth { get; set; }
        public List<RoleTreeDeptOutput> children { get; set; }
    }
    
    public struct DataRang
    {
        public const string All = "全部";
        public const string Dept = "本部门";
        public const string DeptAndBelow = "本部门及以下";
        public const string Self = "仅本人";
        public const string Custom = "自定义";
    }

  
}
