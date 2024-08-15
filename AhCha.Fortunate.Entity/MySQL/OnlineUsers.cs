using SqlSugar;

namespace AhCha.Fortunate.Entity.MySQL
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("online_users")]
    [TenantAttribute(ConstConfigId.MySqlAhChaFortunate)]
    public partial class OnlineUsers
    {
        public OnlineUsers()
        {


        }
        /// <summary>
        /// Desc:雪花id
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, ColumnName = "Id")]
        public long Id { get; set; }

        /// <summary>
        /// Desc:设备id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? DeviceId { get; set; }

        /// <summary>
        /// Desc:用户id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long? UserId { get; set; }

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:True
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Desc:账号
        /// Default:
        /// Nullable:True
        /// </summary>
        public string? Account { get; set; }

        /// <summary>
        /// Desc:客户端IP
        /// Default:
        /// Nullable:False
        /// </summary>
        public string ClientIP { get; set; }

        /// <summary>
        /// Desc:链接id
        /// Default:
        /// Nullable:True
        /// </summary>
        public string? SignalRId { get; set; }

        /// <summary>
        /// Desc:上线时间
        /// Default:
        /// Nullable:True
        /// </summary>
        public DateTime? CreateTime { get; set; }

    }
}
