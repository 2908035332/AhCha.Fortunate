

namespace AhCha.Fortunate.ModelsDto.MSSQL.SysDeptDto
{
    public class SysDeptInput
    {
        public long Id { get; set; }
    }

    public class QuerySysDepInput : SysDeptInput
    {

        public string Name { get; set; }
    }

    public class AddSysDepInput : SysDeptInput
    {
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

    public class PutSysDepInput : SysDeptInput
    {
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

    public class DeleteSysDepInput : SysDeptInput
    {

    }
}
