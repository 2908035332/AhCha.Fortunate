namespace AhCha.Fortunate.ModelsDto.MySQL.OnlineUsersDto
{
    public class OnlineUsersOutput
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
        public string ClientIP { get; set; }

        /// <summary>
        /// 链接id
        /// </summary>
        public string? SignalRId { get; set; }

        /// <summary>
        /// 上线时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}