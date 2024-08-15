using UAParser;
using System.Text.RegularExpressions;

namespace AhCha.Fortunate.Common.Utility
{
    public class HttpUtil
    {

        /// <summary>
        /// 判断IP格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 获取UA的信息
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static UAInfo GetUserAgentInfo(string userAgent)
        {
            ClientInfo clientInfo = Parser.GetDefault().Parse(userAgent);
            UAInfo uaInfo = new UAInfo
            {
                Browser = clientInfo.UA.Family,
                OS = clientInfo.OS.Family,
                Device = clientInfo.Device.Family,
            };
            return uaInfo;
        }
    }

    /// <summary>
    /// UserAgent信息类 https://github.com/ua-parser/uap-csharp
    /// </summary>
    public class UAInfo
    {
        public string Browser { get; set; }
        public string OS { get; set; }
        public string Device { get; set; }
    }
}
