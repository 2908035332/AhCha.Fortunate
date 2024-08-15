
namespace AhCha.Fortunate.ModelsDto.MSSQL.SysDictDataDto
{
    public class SysDictDataOutput
    {
        /// <summary>
        /// Desc:主键
        /// </summary>           
        public long Id { get; set; }

        /// <summary>
        /// Desc:类型ID
        /// </summary>           
        public long TypeId { get; set; }

        /// <summary>
        /// Desc:字典名称
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:字典值1
        /// </summary>           
        public string Value1 { get; set; }

        /// <summary>
        /// Desc:字典值2
        /// </summary>           
        public string Value2 { get; set; }

        /// <summary>
        /// Desc:字典值3
        /// </summary>           
        public string Value3 { get; set; }

        /// <summary>
        /// Desc:是否启用
        /// </summary>           
        public byte? Status { get; set; }

        /// <summary>
        /// Desc:顺序
        /// </summary>           
        public int? Sort { get; set; }

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
