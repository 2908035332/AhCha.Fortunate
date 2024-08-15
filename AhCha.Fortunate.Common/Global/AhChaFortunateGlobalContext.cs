using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AhCha.Fortunate.Common.Global
{
    /// <summary>
    /// 全局统一配置
    /// </summary>
    public class AhChaFortunateGlobalContext
    {
        public static IServiceProvider Instance { get; set; }

        /// <summary>
        /// 获取服务
        /// 最好只获取单例注入的服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>() => Instance.GetService<T>();

        /// <summary>
        /// 是否测试模式，true 是，false 否
        /// </summary>
        public static bool isTest { get; set; }

        /// <summary>
        /// EF Core 数据库链接字符串（MSSQL版本）
        /// </summary>
        public static string AhChaFortunateContext { get; set; }

        /// <summary>
        /// 配置重置统一密码
        /// </summary>
        public static string RestUserPwd { get; set; }

        /// <summary>
        /// 全局配置
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 数据库相关配置
        /// </summary>
        public static List<DatabaseConfig> DatabaseConfigs { get; set; }

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public static String RedisConnection { get; set; }

        /// <summary>
        /// jwt配置
        /// </summary>
        public static JwtSettings JwtSettings { get; set; }

        /// <summary>
        /// Rsa
        /// </summary>
        public static RsaConfig RsaConfig { get; set; }

        /// <summary>
        /// SM2
        /// </summary>
        public static SM2Config SM2Config { get; set; }

        /// <summary>
        /// SM4
        /// </summary>
        public static SM4Config SM4Config { get; set; }

        /// <summary>
        /// Aes加密算法密钥
        /// </summary>
        public static String AesKey { get; set; }

        /// <summary>
        /// 文件存储路径配置
        /// </summary>
        public static DirectoryConfig DirectoryConfig { get; set; }

        /// <summary>
        /// 阿里云Oss配置
        /// </summary>
        public static AliyunOssConfig AliyunOssConfig { get; set; }

        /// <summary>
        /// Swagger配置
        /// </summary>
        public static List<SwaggerConfig> SwaggerConfigs { get; set; }

        /// <summary>
        /// PeriodicTimer定时器配置
        /// </summary>
        public static PeriodicTimerConfig PeriodicTimerConfigs { get; set; }



    }
}
