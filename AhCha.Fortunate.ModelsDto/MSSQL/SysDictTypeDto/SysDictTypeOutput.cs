
namespace AhCha.Fortunate.ModelsDto.MSSQL.SysDictTypeDto
{
    public class SysDictTypeOutput
    {
        /// <summary>
        /// Desc:主键
        /// </summary>  
        public long Id { get; set; }

        /// <summary>
        /// Desc:字典类型名称
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// 字典代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Desc:字典类型描述
        /// </summary>           
        public string Desc { get; set; }

        /// <summary>
        /// Desc:是否启用
        /// </summary>           
        public bool? Status { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:创建人id
        /// </summary>           
        public long? CreateUserId { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// </summary>           
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Desc:修改人id
        /// </summary>           
        public long? UpdateUserId { get; set; }
    }
}
