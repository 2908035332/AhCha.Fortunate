
namespace AhCha.Fortunate.Common.Global
{
    public class JwtSettings
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public String SecretKey { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        public String Issuer { get; set; }

        /// <summary>
        /// 订阅者
        /// </summary>
        public String Audience { get; set; }
    }
}
