
namespace AhCha.Fortunate.Common.Global
{
    /// <summary>
    /// 阿里云oss相关必要配置
    /// </summary>
    public class AliyunOssConfig
    {
        /// <summary>
        /// yourEndpoint填写Bucket所在地域对应的Endpoint。以华东1（杭州）为例，Endpoint填写为https://oss-cn-hangzhou.aliyuncs.com。
        /// </summary>
        public string endpoint { get; set; }

        /// <summary>
        /// 阿里云账号AccessKey拥有所有API的访问权限，风险很高。强烈建议您创建并使用RAM用户进行API访问或日常运维，请登录RAM控制台创建RAM用户。
        /// </summary>
        public string accessKeyId { get; set; }

        /// <summary>
        /// 阿里云账号AccessKey拥有所有API的访问权限，风险很高。强烈建议您创建并使用RAM用户进行API访问或日常运维，请登录RAM控制台创建RAM用户。
        /// </summary>
        public string accessKeySecret { get; set; }

        /// <summary>
        /// 填写Bucket名称，例如examplebucket。
        /// </summary>
        public string bucketName { get; set; }

        /// <summary>
        /// 存储在某个文件夹下
        /// </summary>
        public string path { get; set; }
    }
}
