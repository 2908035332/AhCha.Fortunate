
namespace AhCha.Fortunate.ModelsDto
{
    public class GenerateCoreDto
    {
    }

    public class GenerateCoreInput
    {
        public string ConfigId { get; set; }
        public string TableName { get; set; }
        public string ControllerName { get; set; }
    }



    public class FieldInput
    {
        /// <summary>
        ///字段名称
        /// </summary>
        public string PropName { get; set; }
      
        /// <summary>
        /// 字段类型
        /// </summary>
        public string PropType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string PropDescription { get; set; }

        /// <summary>
        /// 属性类型是否为可空类型
        /// </summary>
        public bool IsNull { get; set; }
    }

    public class GenerateCoreOutput
    {

    }
}
