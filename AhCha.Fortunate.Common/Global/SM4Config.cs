
namespace AhCha.Fortunate.Common.Global
{
    /// <summary>
    /// SM4配置
    /// </summary>
    public class SM4Config
    {
        /// <summary>
        /// 模式
        /// CBC或者ECB
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 偏移量
        /// </summary>
        public string IV { get; set; }
    }

    /// <summary>
    /// SM4模式类型
    /// </summary>
    public class SM4ModelType
    {
        /// <summary>
        /// CBC
        /// </summary>
        public const string CBC = "CBC";

        /// <summary>
        /// ECB
        /// </summary>
        public const string ECB = "ECB";
    }
}
