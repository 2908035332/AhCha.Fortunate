using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;
using AhCha.Fortunate.Common.Global;

namespace AhCha.Fortunate.Common.Utility
{
    /// <summary>
    /// RSA工具类
    /// </summary>
    public static class RsaUtil
    {
        /// <summary>
        /// 使用PEM格式解密（系统默认配置密钥解密）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="decryptstr"></param>
        /// <returns></returns>
        public static T GetPemDecryptEntity<T>(string decryptstr) where T : class, new()
        {
            if (string.IsNullOrEmpty(decryptstr))
                return new T();

            string DecryptEntity = PemDecrypt(decryptstr, AhChaFortunateGlobalContext.RsaConfig.RsaPrivateKey);
            T? TEntity = JsonConvert.DeserializeObject<T>(DecryptEntity);
            return TEntity == null ? new T() : TEntity;
        }

        /// <summary>
        /// 使用PEM格式加密（系统默认配置密钥加密）
        /// </summary>
        /// <param name="encryptstr">需要加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string PemEncrypt(string encryptstr)
        {
            return PemEncrypt(encryptstr, AhChaFortunateGlobalContext.RsaConfig.RsaPublicKey);
        }

        /// <summary>
        /// 使用PEM格式解密（系统默认配置密钥解密）
        /// </summary>
        /// <param name="decryptstr">需要解密的字符串</param>
        /// <returns>返回解密后的字符串</returns>
        public static string PemDecrypt(string decryptstr)
        {
            return PemDecrypt(decryptstr, AhChaFortunateGlobalContext.RsaConfig.RsaPrivateKey);
        }

        /// <summary>
        /// 使用PEM格式解密（自定义密钥解密）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="decryptstr"></param>
        /// <param name="pemprikey"></param>
        /// <returns></returns>
        public static T GetPemDecryptEntity<T>(string decryptstr, string pemprikey) where T : class, new()
        {
            if (string.IsNullOrEmpty(decryptstr))
                return new T();

            string DecryptEntity = PemDecrypt(decryptstr, pemprikey);
            T? TEntity = JsonConvert.DeserializeObject<T>(DecryptEntity);
            return TEntity == null ? new T() : TEntity;
        }

        /// <summary>
        /// 使用PEM格式加密（自定义密钥加密）
        /// </summary>
        /// <param name="pempubkey">pem公钥</param>
        /// <param name="encryptstr">需要加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string PemEncrypt(string encryptstr, string pempubkey)
        {
            RSA rsa = CreateRsaFromPublicKey(pempubkey);
            var plainTextBytes = Encoding.UTF8.GetBytes(encryptstr);
            var cipherBytes = rsa.Encrypt(plainTextBytes, RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// 使用PEM格式解密（自定义密钥解密）
        /// </summary>
        /// <param name="pemprikey">pem格式私钥</param>
        /// <param name="decryptstr">需要解密的字符串</param>
        /// <returns>返回解密后的字符串</returns>
        public static string PemDecrypt(string decryptstr, string pemprikey)
        {
            var rsa = CreateRsaFromPrivateKey(pemprikey);
            var cipherBytes = Convert.FromBase64String(decryptstr);
            var plainTextBytes = rsa.Decrypt(cipherBytes, RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(plainTextBytes);
        }
        private static RSA CreateRsaFromPrivateKey(string privateKey)
        {
            var privateKeyBits = Convert.FromBase64String(privateKey);
            var rsa = RSA.Create();
            var RSAparams = new RSAParameters();

            using (var binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            rsa.ImportParameters(RSAparams);
            return rsa;
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte();
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        private static RSA CreateRsaFromPublicKey(string publicKeyString)
        {
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] x509key;
            byte[] seq = new byte[15];
            int x509size;

            x509key = Convert.FromBase64String(publicKeyString);
            x509size = x509key.Length;

            using (var mem = new MemoryStream(x509key))
            {
                using (var binr = new BinaryReader(mem))
                {
                    byte bt = 0;
                    ushort twobytes = 0;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130)
                        binr.ReadByte();
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();
                    else
                        return null;

                    seq = binr.ReadBytes(15);
                    if (!CompareBytearrays(seq, SeqOID))
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103)
                        binr.ReadByte();
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();
                    else
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x00)
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130)
                        binr.ReadByte();
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102)
                        lowbyte = binr.ReadByte();
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte();
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    int modsize = BitConverter.ToInt32(modint, 0);

                    int firstbyte = binr.PeekChar();
                    if (firstbyte == 0x00)
                    {
                        binr.ReadByte();
                        modsize -= 1;
                    }

                    byte[] modulus = binr.ReadBytes(modsize);

                    if (binr.ReadByte() != 0x02)
                        return null;
                    int expbytes = binr.ReadByte();
                    byte[] exponent = binr.ReadBytes(expbytes);

                    var rsa = RSA.Create();
                    var rsaKeyInfo = new RSAParameters
                    {
                        Modulus = modulus,
                        Exponent = exponent
                    };
                    rsa.ImportParameters(rsaKeyInfo);
                    return rsa;
                }

            }
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        /// <summary>
        /// 生成`RSA`密钥
        /// </summary>
        /// <param name="keySize">512/1024/2048/4096</param>
        /// <returns></returns>
        public static (string PrivateKey, string PublicKey) GenerateRSASecretKey(int keySize = 1024)
        {
            string PrivateKey, PublicKey;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize))
            {
                PrivateKey = rsa.ToXmlString(true);
                PublicKey = rsa.ToXmlString(false);
            }
            return (PrivateKey, PublicKey);
        }

    }
}
