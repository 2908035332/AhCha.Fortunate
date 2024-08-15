using Aliyun.OSS;
using AhCha.Fortunate.Common.Global;

namespace AhCha.Fortunate.Common.Utility
{

    /// <summary>
    /// 阿里云Oss对象存储服务
    /// 请勿公开出去，计费的（RMB）
    /// </summary>
    public class AliyunOssUtil
    {
        /// <summary>
        /// 阿里云oss文件上传
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static PutObjectResult OssUpload(string objectName, Stream stream)
        {
            // 创建OssClient实例。
            OssClient? client = new OssClient(AhChaFortunateGlobalContext.AliyunOssConfig.endpoint, AhChaFortunateGlobalContext.AliyunOssConfig.accessKeyId, AhChaFortunateGlobalContext.AliyunOssConfig.accessKeySecret);
            // 上传文件。
            PutObjectResult result = client.PutObject(AhChaFortunateGlobalContext.AliyunOssConfig.bucketName, string.Concat(AhChaFortunateGlobalContext.AliyunOssConfig.path, objectName), stream);
            stream.Close();
            return result;
        }

        /// <summary>
        ///  阿里云oss文件下载
        /// </summary>
        /// <param name="objectName">文件名称</param>
        /// <returns></returns>
        public static OssObject DownloadOssFile(string objectName)
        {
            // 创建OssClient实例。
            var client = new OssClient(AhChaFortunateGlobalContext.AliyunOssConfig.endpoint, AhChaFortunateGlobalContext.AliyunOssConfig.accessKeyId, AhChaFortunateGlobalContext.AliyunOssConfig.accessKeySecret);
            // 下载文件到流。OssObject包含了文件的各种信息，如文件所在的存储空间、文件名、元信息以及一个输入流。
            OssObject? obj = client.GetObject(AhChaFortunateGlobalContext.AliyunOssConfig.bucketName, string.Concat(AhChaFortunateGlobalContext.AliyunOssConfig.path, objectName));
            return obj;
        }

    }
}
