using System.Text;
using System.Security.Cryptography;

namespace AhCha.Fortunate.Common.Utility
{
    public class PasswordUtil
    {

        public static char SaltCode => '，';

        /// <summary>
        /// 获取新的密码盐码
        /// </summary>
        /// <returns></returns>
        public static string GetPasswordSalt()
        {
            var salt = new byte[128 / 8];
            using (var saltnum = RandomNumberGenerator.Create())
            {
                saltnum.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// 获取根据盐码加密的密码
        /// </summary>
        /// <param name="password">原密码</param>
        /// <param name="salt">盐码</param>
        /// <returns></returns>
        public static string GenEncodingPassword(string password, string salt)
        {
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(password + salt);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder strb = new StringBuilder();
            foreach (byte item in hs)
            {
                strb.Append(item.ToString("x2"));
            }
            return strb.ToString();
        }

        /// <summary>
        /// 分解出加盐码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSalt(string value)
        {
            if (value.StartsWith("ok"))
            {
                return value.Split(SaltCode)[1];
            }
            return string.Empty;
        }
    }
}
