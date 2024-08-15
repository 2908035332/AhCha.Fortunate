

namespace AhCha.Fortunate.Common.Global
{
    /// <summary>
    /// 文件目录配置
    /// </summary>
    public class DirectoryConfig
    {
        /// <summary>
        /// 文件上传存储路径
        /// </summary>
        public string? UploadFilePath { get; set; }

        /// <summary>
        /// 文件最大（M）
        /// </summary>
        public int FileMax { get; set; }

        /// <summary>
        /// 临时文件存储
        /// </summary>
        public string TempPath { get; set; }

        /// <summary>
        /// 限制 （this.FileMax） M
        /// </summary>
        public long limitSiza
        {
            get
            {
                return Convert.ToInt32(this.FileMax) * 1024 * 1024;
            }
        }

    }
}
