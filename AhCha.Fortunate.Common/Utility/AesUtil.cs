using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;
using AhCha.Fortunate.Common.Global;


namespace AhCha.Fortunate.Common.Utility
{
    /// <summary>
    /// AES加解密工具
    /// </summary>
    public class AesUtil
    {
        /// <summary>
        /// 生成Aes密钥key
        /// </summary>
        /// <param name="KeySize">仅支持该设定的长度[128,192,256]</param>
        public static string GenerateAesKey(int KeySize = 128)
        {
            string Key;
            using (AesManaged aes = new AesManaged())
            {
                aes.KeySize = KeySize;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;
                 Key = BitConverter.ToString(aes.Key).Replace("-", "");
            }
            return Key;
        }

        /// <summary>
        /// AES解密（系统配置默认密钥解密）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="decryptstr"></param>
        /// <returns></returns>
        public static T GetDecryptEntity<T>(string decryptstr) where T : class, new()
        {
            if (string.IsNullOrEmpty(decryptstr))
                return new T();

            string DecryptEntity = Decrypt(decryptstr, AhChaFortunateGlobalContext.AesKey);
            T? TEntity = JsonConvert.DeserializeObject<T>(DecryptEntity);
            return TEntity == null ? new T() : TEntity;
        }

        /// <summary>
        /// AES加密（系统配置默认密钥加密）
        /// </summary>
        /// <param name="value">加密串</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string value)
        {
            return Encrypt(value: value, key: "ae125efkk4_54eeff444ferfkny6oxi8", iv: "");
        }

        /// <summary>
        /// AES解密（系统配置默认密钥解密）
        /// </summary>
        /// <param name="value">解密串</param>
        /// <returns>解密后字符串</returns>
        public static string Decrypt(string value)
        {
            return Decrypt(value: value, key: AhChaFortunateGlobalContext.AesKey, iv: "");
        }

        /// <summary>
        /// AES解密（自定义密钥解密）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="decryptstr">解密字符串</param>
        /// <param name="key">aes密钥</param>
        /// <returns></returns>
        public static T GetDecryptEntity<T>(string decryptstr, string key) where T : class, new()
        {
            if (string.IsNullOrEmpty(decryptstr))
                return new T();

            string DecryptEntity = Decrypt(decryptstr, key);
            T? TEntity = JsonConvert.DeserializeObject<T>(DecryptEntity);
            return TEntity == null ? new T() : TEntity;
        }

        /// <summary>
        /// AES加密（自定义密钥）
        /// </summary>
        /// <param name="value">加密串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">偏移量</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string value, string key, string iv = "")
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (key == null)
            {
                throw new Exception("未将对象引用设置到对象的实例。");
            }

            if (key.Length < 16)
            {
                throw new Exception("指定的密钥长度不能少于16位。");
            }

            if (key.Length > 32)
            {
                throw new Exception("指定的密钥长度不能多于32位。");
            }

            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            {
                throw new Exception("指定的密钥长度不明确。");
            }

            if (!string.IsNullOrEmpty(iv))
            {
                if (iv.Length < 16)
                {
                    throw new Exception("指定的向量长度不能少于16位。");
                }
            }

            var _keyByte = Encoding.UTF8.GetBytes(key);
            var _valueByte = Encoding.UTF8.GetBytes(value);

            using (var aes = Aes.Create())
            {
                aes.IV = !string.IsNullOrEmpty(iv) ?
                    Encoding.UTF8.GetBytes(iv) :
                    Encoding.UTF8.GetBytes(key.Reverse().ToString().ToUpper().Substring(0, 16));
                aes.Key = _keyByte;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                var cryptoTransform = aes.CreateEncryptor();
                var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }

        /// <summary>
        /// AES解密（自定义密钥）
        /// </summary>
        /// <param name="value">解密串</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">偏移量</param>
        /// <returns></returns>
        public static string Decrypt(string value, string key, string iv = "")
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (key == null)
            {
                throw new Exception("未将对象引用设置到对象的实例。");
            }

            if (key.Length < 16)
            {
                throw new Exception("指定的密钥长度不能少于16位。");
            }

            if (key.Length > 32)
            {
                throw new Exception("指定的密钥长度不能多于32位。");
            }

            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            {
                throw new Exception("指定的密钥长度不明确。");
            }

            if (!string.IsNullOrEmpty(iv))
            {
                if (iv.Length < 16)
                {
                    throw new Exception("指定的向量长度不能少于16位。");
                }
            }

            var _keyByte = Encoding.UTF8.GetBytes(key);
            var _valueByte = Convert.FromBase64String(value);

            using (var aes = Aes.Create())
            {
                aes.IV = !string.IsNullOrEmpty(iv) ?
                    Encoding.UTF8.GetBytes(iv) :
                    Encoding.UTF8.GetBytes(key.Reverse().ToString().ToUpper().Substring(0, 16));
                aes.Key = _keyByte;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                var cryptoTransform = aes.CreateDecryptor();
                var resultArray = cryptoTransform.TransformFinalBlock(_valueByte, 0, _valueByte.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
        }
    }
}
