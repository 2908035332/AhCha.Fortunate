using System.Text;

using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;

namespace AhCha.Fortunate.Common.Utility
{
    /// <summary>
    /// Aes相关加密密钥生成
    /// </summary>
    public class GenerateKey
    {
        #region 私有只读属性

        /// <summary>
        /// 文件名称
        /// </summary>
        private static string fileName => "AesKey.txt";

        /// <summary>
        /// 存储路径
        /// </summary>
        private static string SavePath => Path.Combine(FileUtil.GetSystemDirectory, fileName);
        
        #endregion

        #region 公开只读属性

        /// <summary>
        /// 获取登录解密Key
        /// </summary>
        public static string GetAesKey
        {
            get
            {
                return FileUtil.FileToString(SavePath, Encoding.UTF8);
            }
        }

        /// <summary>
        /// 是否存在该文件
        /// </summary>
        public static bool IsExistFile => FileUtil.IsExistFile(SavePath);

        #endregion

        /// <summary>
        /// 生成AesKey
        /// </summary>
        /// <param name="KeySize">仅支持该设定的长度[128,192,256]</param>
        /// <returns></returns>
        public static string SetAesKey(int KeySize = 128)
        {
            string AesKey = AesUtil.GenerateAesKey(KeySize);
            //创建目录
            FileUtil.CreateDirectory(FileUtil.GetSystemDirectory);
            //创建文件
            FileUtil.CreateFile(SavePath);
            //写入内容
            FileUtil.WriteText(SavePath, AesKey);
            return AesKey;
        }

        /// <summary>
        /// 生成系统所需要的AesKey
        /// </summary>
        /// <param name="KeySize"></param>
        public static void SetAesKey(string BaseDirectory)
        {
            try
            {
                string AesKey = AesUtil.GenerateAesKey(128);
                //创建目录
                FileUtil.CreateDirectory(BaseDirectory);
                //创建文件
                FileUtil.CreateFile(Path.Combine(BaseDirectory, fileName));
                //写入内容
                FileUtil.WriteText(Path.Combine(BaseDirectory, fileName), AesKey);
            }
            catch (Exception ex)
            {
                LogUtil.Error($"{DateTime.Now}：BaseDirectory：{BaseDirectory}，执行报错 \r\n \r\n ${JsonConvert.SerializeObject(ex)}");
            }
        }

        #region MyRegion
        /*
        /// <summary>
        /// 定义委托
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private delegate string GenerateKeyDelegate(string? value);

        private Dictionary<string, GenerateKeyDelegate> actions;
        public GenerateKey()
        {
            actions = new Dictionary<string, GenerateKeyDelegate>
            {
                { "1", (input)=>  function1(input) },
            };
        }

        private string function1(string input)
        {
            return input;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="key">id</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> ExecuteAction(string key,string value)
        {
            if (actions.TryGetValue(key, out GenerateKeyDelegate action))
            {
                return action(value);
            }
            else
            {
                return "异常";
            }
        }
        */
        #endregion

    }
}
