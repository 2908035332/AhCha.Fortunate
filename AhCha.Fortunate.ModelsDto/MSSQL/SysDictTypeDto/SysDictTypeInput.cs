
namespace AhCha.Fortunate.ModelsDto.MSSQL.SysDictTypeDto
{
    public class SysDictTypeInput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
    }

    public class QuerySysDictTypeInput : PageInputBase
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartQueryTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndQueryTime { get; set; }

        /// <summary>
        /// 字典类型名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 字典代码
        /// </summary>
        public string? Code { get; set; }
    }
    public class AddSysDictTypeInput
    {
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

        //public List<AddSysDictDataInput>? DictDatas { get; set; }
        public List<object>? DictDatas { get; set; }
    }

    public class PutSysDictTypeInput
    {

        /// <summary>
        /// 主键id
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

        //public List<AddSysDictDataInput>? DictDatas { get; set; }
        public List<object>? DictDatas { get; set; }
    }

    public class DeleteSysDictTypeInput : SysDictTypeInput
    {

    }


    public class SearchInput
    {
        public string Code { get; set; }
    }
}
