namespace AhCha.Fortunate.ModelsDto.MySQL.OnlineUsersDto
{
    public class OnlineUsersInput
    {
        public long Id { get; set; }
    }

    public class QueryOnlineUsersInput : PageInputBase
    {

    }

    public class PostOnlineUsersInput
    {

        /// <summary>
        /// 雪花id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        public string? DeviceId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string? Account { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string? ClientIP { get; set; }

        /// <summary>
        /// 链接id
        /// </summary>
        public string? SignalRId { get; set; }

        /// <summary>
        /// 上线时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }

    public class DeleteOnlineUsersInput 
    {
        /// <summary>
        /// 链接id
        /// </summary>
        public string? SignalRId { get; set; }
    }
}