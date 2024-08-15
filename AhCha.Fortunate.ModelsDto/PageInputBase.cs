
namespace AhCha.Fortunate.ModelsDto
{
    /// <summary>
    /// 所有的QueryInput 都应该继承该类
    /// 统一管理 页码、大小字段名
    /// </summary>
    public class PageInputBase
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public virtual int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页码容量
        /// </summary>
        public virtual int PageSize { get; set; } = 20;
    }

    public class SelectHelper
    {
        public string? value { get; set; }
        public string? label { get; set; }
        public long? parentId { get; set; }
        public List<SelectHelper> children { get; set; }
    }


}
