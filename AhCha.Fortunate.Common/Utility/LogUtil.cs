using AhCha.Fortunate.Common.Global;

namespace AhCha.Fortunate.Common.Utility
{

    /// <summary>
    /// 日志输出工具类
    /// </summary>
    public class LogUtil
    {
        static string _log = "Logs";

        /// <summary>
        /// 信息日志输出
        /// </summary>
        /// <param name="Content">输出的内容</param>
        /// <param name="directoryPath">输出的路径</param>
        public static void Info(string content, string directoryPath = "")
        {
            string fileName = string.Concat(DateTime.Now.ToString("yyyy-MM-dd"), ".txt");
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                directoryPath = Path.Combine(FileUtil.GetSystemDirectory, _log, "Information");
            }
            IsExistDirectory(directoryPath, fileName);
            FileUtil.AppendText(Path.Combine(directoryPath, fileName), content + "\r\n \r\n");
        }

        /// <summary>
        /// 警告日志输出
        /// </summary>
        /// <param name="Content">输出的内容</param>
        /// <param name="directoryPath">输出的路径</param>
        public static void Warning(string content, string directoryPath = "")
        {
            string fileName = string.Concat(DateTime.Now.ToString("yyyy-MM-dd"), ".txt");
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                directoryPath = Path.Combine(FileUtil.GetSystemDirectory, _log, "Warning");
            }
            IsExistDirectory(directoryPath, fileName);
            FileUtil.AppendText(Path.Combine(directoryPath, fileName), content + "\r\n \r\n");
        }

        /// <summary>
        /// 异常日志输出
        /// </summary>
        /// <param name="Content">输出的内容</param>
        /// <param name="directoryPath">输出的路径</param>
        public static void Error(string content, string directoryPath = "")
        {
            string fileName = string.Concat(DateTime.Now.ToString("yyyy-MM-dd"), ".txt");
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                directoryPath = Path.Combine(FileUtil.GetSystemDirectory, _log, "Error");
            }
            IsExistDirectory(directoryPath, fileName);
            FileUtil.AppendText(Path.Combine(directoryPath, fileName), content + "\r\n \r\n");
        }

        /// <summary>
        /// 调式日志输出
        /// </summary>
        /// <param name="Content">输出的内容</param>
        /// <param name="directoryPath">输出的路径</param>
        public static void Debug(string content, string directoryPath = "")
        {
            string fileName = string.Concat(DateTime.Now.ToString("yyyy-MM-dd"), ".txt");
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                directoryPath = Path.Combine(FileUtil.GetSystemDirectory, _log, "Debug");
            }
            IsExistDirectory(directoryPath, fileName);
            FileUtil.AppendText(Path.Combine(directoryPath, fileName), content + "\r\n \r\n");
        }

        private static void IsExistDirectory(string directoryPath, string fileName)
        {
            FileUtil.CreateDirectory(directoryPath);
            FileUtil.CreateFile(Path.Combine(directoryPath, fileName));
        }

    }
}
