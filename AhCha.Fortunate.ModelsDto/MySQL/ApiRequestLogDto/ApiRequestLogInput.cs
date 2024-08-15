namespace AhCha.Fortunate.ModelsDto.MySQL.ApiRequestLogDto
{
    public class ApiRequestLogInput
    {
        public long Id { get; set; }
    }

    public class QueryApiRequestLogInput : PageInputBase
    {

    }

    public class PostApiRequestLogInput
    {

        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string? ControllerName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string? ActionName { get; set; }

        /// <summary>
        /// 请求的参数JSON
        /// </summary>
        public string? Param { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public string? Ip { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string? Method { get; set; }

        /// <summary>
        /// 主机
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public long? CreateUserId { get; set; }

    }

    public class PutApiRequestLogInput
    {

        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string? ControllerName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string? ActionName { get; set; }

        /// <summary>
        /// 请求的参数JSON
        /// </summary>
        public string? Param { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public string? Ip { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string? Method { get; set; }

        /// <summary>
        /// 主机
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public long? CreateUserId { get; set; }

    }

    public class DeleteApiRequestLogInput : ApiRequestLogInput
    {

    }
}