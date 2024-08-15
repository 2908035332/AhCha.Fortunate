namespace AhCha.Fortunate.Common
{
    /// <summary>
    /// 生成所需要的帮助类
    /// </summary>
    public class GenerateCore
    {
        public static string[] GetMethods => new string[] { "Page", "Post", "Put", "Delete", "Detail" };

        /// <summary>
        ///数据库实体命名空间
        /// </summary>
        public const string GetTEntityNameSpace = "AhCha.Fortunate.Entity.";


        /// <summary>
        /// TEntityDto模板
        /// </summary>
        public const string TEntityInputTemplate = "TEntityDtoInputTemplate.txt";
        public const string TEntityOutputTemplate = "TEntityDtoOutputTemplate.txt";
        /// <summary>
        /// Dto命名空间
        /// </summary>
        public const string GetITEntityDtoNameSpace = "AhCha.Fortunate.ModelsDto.";


        /// <summary>
        /// ITEntityService模板
        /// </summary>
        public const string TEntityIServiceTemplate = "TEntityIServiceTemplate.txt";
        /// <summary>
        /// 获取ITEntityService 命名空间
        /// </summary>
        public const string GetITEntityServiceNameSpace = "AhCha.Fortunate.IService.";


        /// <summary>
        /// TEntityService模板
        /// </summary>
        public const string TEntityServiceTemplate = "TEntityServiceTemplate.txt";
        /// <summary>
        /// 获取TEntityService 命名空间
        /// </summary>
        public const string GetTEntityServiceNameSpace = "AhCha.Fortunate.Service.";


        /// <summary>
        /// 获取TEntityController模板
        /// </summary>
        public const string TEntityControllerTemplate = "TEntityControllerTemplate.txt";
        /// <summary>
        /// 获取TEntityController 命名空间
        /// </summary>
        public const string GetTEntityControllerNameSpace = "AhCha.Fortunate.Api.Controllers.";



        public static string SetName { get; set; }

        public static string GetControllerName => string.Concat(SetName, "Controller.cs");
        public static string GetServiceName => string.Concat(SetName, "Service.cs");
        public static string GetIServiceName => string.Concat("I", SetName, "Service.cs");
        public static string GetDtoName => string.Concat(SetName, "Dto");
        public static string GetDtoInputName => string.Concat(SetName, "Input.cs");
        public static string GetDtoOutputName => string.Concat(SetName, "Output.cs");
    }
}
