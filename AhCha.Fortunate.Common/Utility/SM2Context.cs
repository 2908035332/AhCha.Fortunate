using KYSharp.SM;
using System.Text;
using AhCha.Fortunate.Common.Global;

namespace AhCha.Fortunate.Common.Utility
{
    /// <summary>
    /// SM2会话工具
    /// </summary>
    public class SM2Context
    {

        /// <summary>
        /// 可生成公私钥
        /// </summary>
        public static void GenerateKey()
        {
            SM2Utils.GenerateKeyPair(out string publickey, out string privatekey);//生成公钥和私钥对
        }

        /// <summary>
        /// 公钥加密明文
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string plainText)
        {
            return SM2Utils.Encrypt_Hex(AhChaFortunateGlobalContext.SM2Config.PublicKey, plainText, Encoding.UTF8);
        }

        /// <summary>
        /// 私钥解密密文
        /// </summary>
        /// <param name="cipherText">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string cipherText)
        {
            return SM2Utils.Decrypt_Hex(AhChaFortunateGlobalContext.SM2Config.PrivateKey, cipherText, Encoding.UTF8);
        }

    }
}
