using TinyPinyin;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using Microsoft.International.Converters.PinYinConverter;

namespace AhCha.Fortunate.Common.Extensions
{
    /// <summary>
    /// 系统统一管理扩展方法
    /// </summary>
    public static class FortunateExtensions
    {
        public static String ToYear(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 数组字符串转为string数组
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="SplitStr">默认,</param>
        /// <returns></returns>
        public static String[] GetStrings(this String ids, char SplitStr = ',')
        {
            if (!String.IsNullOrEmpty(ids))
            {
                var arrStrs = ids.Split(SplitStr);
                var arrGuids = new string[arrStrs.Length];

                for (var i = 0; i < arrStrs.Length; i++)
                {
                    arrGuids[i] = arrStrs[i].ToString();
                }

                return arrGuids;
            }

            return null;
        }

        /// <summary>
        /// string字符串转Guid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid ToGuid(this String value)
        {
            if (Guid.TryParse(value, out Guid GuidValue))
            {
                return GuidValue;
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// 数组字符串转为Guid数组
        /// </summary>
        /// <param name="ids">数组字符串</param>
        /// <returns>Guid数组</returns>
        public static Guid[] GetGuids(String ids)
        {
            if (!String.IsNullOrEmpty(ids))
            {
                var arrStrs = ids.Split(',');

                var arrGuids = new Guid[arrStrs.Length];

                for (var i = 0; i < arrStrs.Length; i++)
                {
                    arrGuids[i] = Guid.Parse(arrStrs[i]);
                }

                return arrGuids;
            }

            return null;
        }

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="obj">Object对象</param>
        /// <returns>bool</returns>
        public static Boolean IsNullOrEmpty(Object obj)
        {
            var result = false;

            if (obj == null)
            {
                result = true;
            }
            else
            {
                if (String.IsNullOrEmpty(obj.ToString()))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 判断是否包含字符串
        /// </summary>
        /// <param name="container">容器</param>
        /// <param name="obj">Object对象</param>
        /// <returns>bool</returns>
        public static Boolean IsContains(String[] container, Object obj)
        {
            var result = false;

            if (container.Length > 0 && obj != null)
            {
                if (container.Contains(obj.ToString()))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 类型枚举转为字典
        /// </summary>
        /// <param name="textEnum">类型枚举</param>
        /// <returns>字典</returns>
        public static Dictionary<String, String> ToDictionary(this String textEnum)
        {
            Dictionary<String, String> result = new Dictionary<String, String>();

            if (!String.IsNullOrEmpty(textEnum))
            {
                var array = textEnum.Split(",");
                foreach (var text in array)
                {
                    var subArray = text.Split(":");
                    result.Add(subArray[0].ToString(), subArray[1].ToString());
                }
            }

            return result;
        }

        /// <summary>
        /// 自定义去重复
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IEnumerable<T> DistinctBy<T, TResult>(this IEnumerable<T> source, Func<T, TResult> where)
        {
            HashSet<TResult> hashSetData = new HashSet<TResult>();
            foreach (T item in source)
            {
                if (hashSetData.Add(where(item)))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// 是否base64格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsBase64String(this String str)
        {
            str = str.Trim();
            return (str.Length % 4 == 0) && Regex.IsMatch(str, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        /// <summary>
        /// ImageToBase64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String ImageToBase64(this String str)
        {
            try
            {
                Bitmap bmp = new Bitmap(str);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Png);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region 转换为long

        /// <summary>
        /// 将object转换为long，若转换失败，则返回0。不抛出异常。  
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ParseToLong(this object obj)
        {
            try
            {
                return long.Parse(obj.ToString() ?? string.Empty);
            }
            catch
            {
                return 0L;
            }
        }

        /// <summary>
        /// 将object转换为long，若转换失败，则返回指定值。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ParseToLong(this string str, long defaultValue)
        {
            try
            {
                return long.Parse(str);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion

        #region 转换为int

        /// <summary>
        /// 将object转换为int，若转换失败，则返回0。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ParseToInt(this object str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 将object转换为int，若转换失败，则返回指定值。不抛出异常。 
        /// null返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ParseToInt(this object str, int defaultValue)
        {
            if (str == null)
            {
                return defaultValue;
            }

            try
            {
                return Convert.ToInt32(str);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion

        #region 转换为short

        /// <summary>
        /// 将object转换为short，若转换失败，则返回0。不抛出异常。  
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static short ParseToShort(this object obj)
        {
            try
            {
                return short.Parse(obj.ToString() ?? string.Empty);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 将object转换为short，若转换失败，则返回指定值。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ParseToShort(this object str, short defaultValue)
        {
            try
            {
                return short.Parse(str.ToString() ?? string.Empty);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion

        #region 转换为demical

        /// <summary>
        /// 将object转换为demical，若转换失败，则返回指定值。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ParseToDecimal(this object str, decimal defaultValue)
        {
            try
            {
                return decimal.Parse(str.ToString() ?? string.Empty);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将object转换为demical，若转换失败，则返回0。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ParseToDecimal(this object str)
        {
            try
            {
                return decimal.Parse(str.ToString() ?? string.Empty);
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region 转化为bool

        /// <summary>
        /// 将object转换为bool，若转换失败，则返回false。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ParseToBool(this object str)
        {
            try
            {
                return bool.Parse(str.ToString() ?? string.Empty);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将object转换为bool，若转换失败，则返回指定值。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool ParseToBool(this object str, bool result)
        {
            try
            {
                return bool.Parse(str.ToString() ?? string.Empty);
            }
            catch
            {
                return result;
            }
        }

        #endregion

        #region 转换为float

        /// <summary>
        /// 将object转换为float，若转换失败，则返回0。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ParseToFloat(this object str)
        {
            try
            {
                return float.Parse(str.ToString() ?? string.Empty);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 将object转换为float，若转换失败，则返回指定值。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static float ParseToFloat(this object str, float result)
        {
            try
            {
                return float.Parse(str.ToString() ?? string.Empty);
            }
            catch
            {
                return result;
            }
        }

        #endregion

        #region 转换为Guid

        /// <summary>
        /// 将string转换为Guid，若转换失败，则返回Guid.Empty。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ParseToGuid(this string str)
        {
            try
            {
                return new Guid(str);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        #endregion

        #region 转换为DateTime

        /// <summary>
        /// 将string转换为DateTime，若转换失败，则返回日期最小值。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ParseToDateTime(this string str)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return DateTime.MinValue;
                }

                if (str.Contains("-") || str.Contains("/"))
                {
                    return DateTime.Parse(str);
                }

                var length = str.Length;
                return length switch
                {
                    4 => DateTime.ParseExact(str, "yyyy", CultureInfo.CurrentCulture),
                    6 => DateTime.ParseExact(str, "yyyyMM", CultureInfo.CurrentCulture),
                    8 => DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.CurrentCulture),
                    10 => DateTime.ParseExact(str, "yyyyMMddHH", CultureInfo.CurrentCulture),
                    12 => DateTime.ParseExact(str, "yyyyMMddHHmm", CultureInfo.CurrentCulture),
                    // ReSharper disable once StringLiteralTypo
                    14 => DateTime.ParseExact(str, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),
                    // ReSharper disable once StringLiteralTypo
                    _ => DateTime.ParseExact(str, "yyyyMMddHHmmss", CultureInfo.CurrentCulture)
                };
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 将string转换为DateTime，若转换失败，则返回默认值。  
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ParseToDateTime(this string str, DateTime? defaultValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return defaultValue.GetValueOrDefault();
                }

                if (str.Contains("-") || str.Contains("/"))
                {
                    return DateTime.Parse(str);
                }

                var length = str.Length;
                return length switch
                {
                    4 => DateTime.ParseExact(str, "yyyy", CultureInfo.CurrentCulture),
                    6 => DateTime.ParseExact(str, "yyyyMM", CultureInfo.CurrentCulture),
                    8 => DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.CurrentCulture),
                    10 => DateTime.ParseExact(str, "yyyyMMddHH", CultureInfo.CurrentCulture),
                    12 => DateTime.ParseExact(str, "yyyyMMddHHmm", CultureInfo.CurrentCulture),
                    // ReSharper disable once StringLiteralTypo
                    14 => DateTime.ParseExact(str, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),
                    // ReSharper disable once StringLiteralTypo
                    _ => DateTime.ParseExact(str, "yyyyMMddHHmmss", CultureInfo.CurrentCulture)
                };
            }
            catch
            {
                return defaultValue.GetValueOrDefault();
            }
        }

        #endregion

        #region 转换为string

        /// <summary>
        /// 将object转换为string，若转换失败，则返回""。不抛出异常。  
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ParseToString(this object obj)
        {
            try
            {
                return obj == null ? string.Empty : obj.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ParseToStrings<T>(this object obj)
        {
            try
            {
                if (obj is IEnumerable<T> list)
                {
                    return string.Join(",", list);
                }

                return obj.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

        #region 转换为double

        /// <summary>
        /// 将object转换为double，若转换失败，则返回0。不抛出异常。  
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ParseToDouble(this object obj)
        {
            try
            {
                return double.Parse(obj.ToString() ?? string.Empty);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 将object转换为double，若转换失败，则返回指定值。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ParseToDouble(this object str, double defaultValue)
        {
            try
            {
                return double.Parse(str.ToString() ?? string.Empty);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion

        #region 强制转换类型

        /// <summary>
        /// 强制转换类型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> CastSuper<TResult>(this IEnumerable source)
        {
            return from object item in source select (TResult)Convert.ChangeType(item, typeof(TResult));
        }

        #endregion

        #region 转换为ToUnixTime

        public static long ParseToUnixTime(this DateTime nowTime)
        {
            var startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }

        #endregion

        #region 字符串转Enum
        /// <summary>
        /// 字符串转Enum
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="str">字符串</param>
        /// <returns>转换的枚举</returns>
        public static T ToEnum<T>(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentNullException("字典值缺失！");
            }
            return (T)System.Enum.Parse(typeof(T), str);
        }
        #endregion

        /// <summary>
        /// 获取枚举的描述信息
        /// </summary>
        public static String GetDescription(this System.Enum em)
        {
            Type type = em.GetType();
            FieldInfo? fd = type.GetField(em.ToString());
            if (fd == null)
                return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(DescriptionAttribute), false);
            string txt = string.Empty;
            foreach (DescriptionAttribute attr in attrs)
            {
                txt = attr.Description;
            }
            return txt;
        }


        #region 脱敏处理

        /// <summary>
        /// 将传入的字符串中间部分字符替换成特殊字符
        /// </summary>
        /// <param name="value">需要替换的字符串</param>
        /// <param name="startLen">前保留长度，默认4</param>
        /// <param name="subLen">要替换的长度，默认4</param>
        /// <param name="specialChar">特殊字符，默认为*</param>
        /// <returns>替换后的结果</returns>
        public static String ReplaceWithSpecialChar(this String value, int startLen = 4, int subLen = 4, char specialChar = '*')
        {
            if (value.Length <= startLen + subLen) return value;

            string startStr = value.Substring(0, startLen);
            string endStr = value.Substring(startLen + subLen);
            string specialStr = new string(specialChar, subLen);

            return startStr + specialStr + endStr;
        }

        /// <summary>
        /// 数据脱敏
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="left">左显示字符数量</param>
        /// <param name="right">右显示字符数量</param>
        /// <returns></returns>
        public static String ToDataMasking(this String str, int left = 3, int right = 3)
        {
            if (string.IsNullOrEmpty(str) || str.Length < (left + right))
                return str;

            var chars = str.ToCharArray();
            for (int i = left; i < chars.Length - right; i++)
                chars[i] = '*';

            return new string(chars);
        }

        /// <summary>
        /// 姓名敏感处理
        /// </summary>
        /// <param name="fullName">姓名</param>
        /// <returns>脱敏后的姓名</returns>
        public static String SetSensitiveName(this String value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            string BeginStr = value.Substring(0, 1);
            string EndStr = value.Substring(value.Length - 1, 1);
            string All = string.Empty;
            if (value.Length <= 2) All = "*" + EndStr;
            else if (value.Length >= 3)
            {
                All = BeginStr.PadRight(value.Length - 1, '*') + EndStr;
            }
            return All;
        }

        /// <summary>
        /// 身份证脱敏
        /// </summary>
        /// <param name="value">身份证号</param>
        /// <returns>脱敏后的身份证号</returns>
        public static String SetSensitiveIdCard(this String value)
        {
            if (string.IsNullOrEmpty(value)
                   || (value.Length != 15 && value.Length != 18)) return value;

            string BeginStr = value.Substring(0, 3);
            string EndStr = value.Substring(16, value.Length - 16);
            return string.Concat(BeginStr, "********", EndStr);
        }

        /// <summary>
        /// 手机脱敏
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String SetSensitivePhone(this String value)
        {
            return String.IsNullOrEmpty(value) ? value : value.Substring(0, 2) + "****" + value.Substring(9, 2);
        }

        /// <summary>
        /// 邮箱敏感处理
        /// </summary>
        /// <param name="fullName">邮箱</param>
        /// <returns>脱敏后的email</returns>
        public static String SetSensitiveEmail(this String value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.IndexOf('@') <= 0) return string.Empty;
            string emailSuffix = value.Substring(value.IndexOf('@'));
            string partStr = value.Substring(0, value.IndexOf('@'));
            string beginStr = partStr.Substring(0, 1);
            string endStr = partStr.Substring(partStr.Length - 1, 1);
            string text = string.Empty;
            if (partStr.Length <= 2) text = beginStr + "*";
            else if (partStr.Length >= 3)
            {
                text = beginStr.PadRight(partStr.Length - 1, '*') + endStr;
            }
            return string.Concat(text, emailSuffix);
        }


        #endregion

        #region 汉字转拼音
        /// <summary> 
        /// 汉字转化为拼音（PinYinConverterCore）
        /// </summary> 
        /// <param name="values">汉字</param> 
        /// <returns>汉字全拼</returns> 
        public static String ToPinYinAll(this String values)
        {
            string r = string.Empty;
            foreach (char obj in values)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r.ToLower();
        }

        /// <summary> 
        /// 汉字转化为拼音首字母（PinYinConverterCore）
        /// </summary> 
        /// <param name="values">汉字</param> 
        /// <returns>汉字首字母</returns> 
        public static String ToPinYinFirst(this String values)
        {
            string r = string.Empty;
            foreach (char obj in values)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r.ToLower();
        }

        /// <summary>
        /// 汉字转化为拼音（TinyPinyin.Net）
        /// </summary>
        /// <param name="value">汉字</param>
        /// <returns></returns>
        public static String ToPinYin(this String value)
        {
            return PinyinHelper.GetPinyin(value).Trim().ToLower();
        }

        /// <summary>
        /// 汉字转拼音（TinyPinyin.Net）
        /// </summary>
        /// <param name="value">汉字</param>
        /// <returns>汉字首字母</returns>
        public static String ToPinYinInitials(this String value)
        {
            return PinyinHelper.GetPinyinInitials(value).Trim().ToLower();
        }
        #endregion

        /// <summary>
        /// 是否为空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 不为空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 检查 Object 是否为 NULL
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this object value)
        {
            return value == null || string.IsNullOrEmpty(value.ParseToString());
        }

        /// <summary>
        /// 检查 Object 是否为 NULL 或者 0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrZero(this object value)
        {
            return value == null || value.ParseToString().Trim() == "0";
        }

        public static bool IsNullOrZero(this long? value)
        {
            return value == null || value == 0;
        }

        /// <summary>
        /// 检查int?不为NULL且不为0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrZero(this int? value)
        {
            return value != null && value != 0;
        }

        /// <summary>
        /// 检查long?不为NULL且不为0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrZero(this long? value)
        {
            return value != null && value != 0;
        }

        /// <summary>
        /// 简单下划线转驼峰命名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToHump(this String value)
        {
            string str = string.Empty;
            if (value.Contains('_'))
            {
                string[] arr = value.Split("_").ToArray();
                foreach (var item in arr)
                {
                    string one = item.Substring(0, 1).ToUpper();
                    string end = item.Substring(1, item.Length - 1);
                    str += string.Concat(one, end);
                }
            }
            else
            {
                string one = value.Substring(0, 1).ToUpper();
                string end = value.Substring(1, value.Length - 1);
                str = string.Concat(one, end);
            }
            return str;
        }

        /// <summary>
        /// 数据类型转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToDataType(this String value)
        {
            switch (value.ToLower())
            {
                case "text":
                case "varchar":
                case "char":
                case "nvarchar":
                case "nchar":
                case "timestamp":
                    return "string";
                case "int":
                    return "int";
                case "smallint":
                    return "Int16";
                case "tinyint":
                    return "byte";
                case "bigint":
                case "integer"://sqlite数据库
                    return "long";
                case "bit":
                    return "bool";
                case "money":
                case "smallmoney":
                case "numeric":
                case "decimal":
                    return "decimal";
                case "real":
                    return "Single";
                case "datetime":
                case "smalldatetime":
                    return "DateTime";
                case "float":
                    return "double";
                case "image":
                case "binary":
                case "varbinary":
                    return "byte[]";
                case "uniqueidentifier":
                    return "Guid";
                default:
                    return "object";
            }
        }

        /// <summary>
        /// object转字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Dictionary<string, object> ConvertToDictionary<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            return obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToDictionary(prop => prop.Name, prop => prop.GetValue(obj));
        }

        #region EntityFrameworkCore 扩展

        /// <summary>
        /// 扩展 EF 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Source"></param>
        /// <param name="isWhere">true，升序，false倒叙</param>
        /// <param name="OrderByA">根据自定义字段A排序后</param>
        /// <param name="OrderByB">在根据自定义字段B排序</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderThenBy<T>(this IQueryable<T> Source, bool isWhere, Expression<Func<T, object>> OrderByA, Expression<Func<T, object>> OrderByB)
        {
            if (isWhere)
                return Source.OrderBy(OrderByA).ThenBy(OrderByB);
            else
                return Source.OrderByDescending(OrderByA).ThenByDescending(OrderByB);
        }
        
        /// <summary>
        /// 扩展 EF Core Where
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="Source"></param>
        /// <param name="isWhere"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIF<TSource>(this IQueryable<TSource> Source, bool isWhere, Expression<Func<TSource, bool>> predicate)
        {
            if (isWhere)
            {
                Source = Source.Where(predicate);
            }
            return Source;
        }

        #endregion

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetIp(this HttpContext httpContext)
        {
            string ipAddress = httpContext.Connection.RemoteIpAddress.ToString();
            if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                string[] forwardedIps = httpContext.Request.Headers["X-Forwarded-For"].ToString().Split(',');
                ipAddress = forwardedIps.FirstOrDefault()?.Trim();
            }
            if (httpContext.Request.Headers.ContainsKey("X-Real-IP"))
            {
                string[] forwardedIps = httpContext.Request.Headers["X-Real-IP"].ToString().Split(',');
                string realIp = forwardedIps.FirstOrDefault()?.Trim();
                if (!string.IsNullOrEmpty(realIp) && realIp.Length > 7 && !realIp.StartsWith("127.0") && !realIp.StartsWith("192.168"))//代理、内网穿透等情况
                {
                    ipAddress = realIp;
                }
            }
            if (ipAddress.Contains("::1"))
                ipAddress = "127.0.0.1";
            return ipAddress.Replace("::ffff:", "");
        }

        /// <summary>
        /// 获取UA标识
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetUserAgent(this HttpContext httpContext)
        {
            return httpContext.Request.Headers["User-Agent"];
        }

    }

}
