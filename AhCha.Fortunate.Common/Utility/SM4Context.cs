using KYSharp.SM;
using AhCha.Fortunate.Common.Global;

namespace AhCha.Fortunate.Common.Utility
{
    /// <summary>
    /// SM4会话工具
    /// </summary>
    public class SM4Context
    {
        /// <summary>
        /// 加密明文
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string plainText)
        {
            string cipherText = string.Empty;

            SM4Utils sm4 = new SM4Utils();
            sm4.secretKey = AhChaFortunateGlobalContext.SM4Config.SecretKey;
            sm4.hexString = false;
            sm4.iv = AhChaFortunateGlobalContext.SM4Config.IV.Reverse().ToString();

            switch (AhChaFortunateGlobalContext.SM4Config.Model)
            {
                case SM4ModelType.CBC:
                    cipherText = sm4.Encrypt_CBC(plainText);
                    break;
                case SM4ModelType.ECB:
                    cipherText = sm4.Encrypt_ECB(plainText);
                    break;
            }

            return cipherText;
        }

        /// <summary>
        /// 解密密文
        /// </summary>
        /// <param name="cipherText">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string cipherText)
        {
            string plainText = string.Empty;

            SM4Utils sm4 = new SM4Utils();
            sm4.secretKey = AhChaFortunateGlobalContext.SM4Config.SecretKey;
            sm4.hexString = false;
            sm4.iv = AhChaFortunateGlobalContext.SM4Config.IV.Reverse().ToString();

            switch (AhChaFortunateGlobalContext.SM4Config.Model)
            {
                case SM4ModelType.CBC:
                    plainText = sm4.Decrypt_CBC(cipherText);
                    break;
                case SM4ModelType.ECB:
                    plainText = sm4.Decrypt_ECB(cipherText);
                    break;
            }

            return plainText;
        }
    }
}
