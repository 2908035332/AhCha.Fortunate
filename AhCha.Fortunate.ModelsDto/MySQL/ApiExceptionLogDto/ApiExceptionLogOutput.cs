namespace AhCha.Fortunate.ModelsDto.MySQL.ApiExceptionLogDto
{
    public class ApiExceptionLogOutput
    {

        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Controller名称
        /// </summary>
        public string? ControllerName { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        public string? ActionName { get; set; }

        /// <summary>
        /// 调用时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 用户ip地址
        /// </summary>
        public string? Ip { get; set; }

        /// <summary>
        /// 异常记录
        /// </summary>
        public string? ExceptionText { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long? CreateUserId { get; set; }

    }
}