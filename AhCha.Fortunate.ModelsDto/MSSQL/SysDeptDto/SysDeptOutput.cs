
namespace AhCha.Fortunate.ModelsDto.MSSQL.SysDeptDto
{
    public class SysDeptOutput
    {
        public long Id { get; set; }
        /// <summary>
        /// Desc:父级部门ID
        /// </summary>           
        public long ParentId { get; set; }

        /// <summary>
        /// Desc:部门名称
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
    }

    public class TreeDeptOutput
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public List<TreeDeptOutput> children { get; set; }
    }
}
