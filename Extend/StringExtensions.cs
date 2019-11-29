using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static partial class StringExtensions
    {
        #region 正则表达式

        /// <summary>
        /// 指示所指定的正则表达式在指定的输入字符串中是否找到了匹配项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>如果正则表达式找到匹配项，则为 true；否则，为 false</returns>
        public static bool IsMatch(this string value, string pattern)
        {
            if (value == null)
            {
                return false;
            }
            return Regex.IsMatch(value, pattern);
        }

        /// <summary>
        /// 验证是否IP地址
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIPAddress(this string value)
        {
            string pattrn = @"(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])";
            if (System.Text.RegularExpressions.Regex.IsMatch(value, pattrn))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的第一个匹配项
        /// </summary>
        /// <param name="value">要搜索匹配项的字符串</param>
        /// <param name="pattern">要匹配的正则表达式模式</param>
        /// <returns>一个对象，包含有关匹配项的信息</returns>
        public static string Match(this string value, string pattern)
        {
            if (value == null)
            {
                return null;
            }
            return Regex.Match(value, pattern).Value;
        }

        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的所有匹配项的字符串集合
        /// </summary>
        /// <param name="value"> 要搜索匹配项的字符串 </param>
        /// <param name="pattern"> 要匹配的正则表达式模式 </param>
        /// <returns> 一个集合，包含有关匹配项的字符串值 </returns>
        public static IEnumerable<string> Matches(this string value, string pattern)
        {
            if (value == null)
            {
                return new string[] { };
            }
            MatchCollection matches = Regex.Matches(value, pattern);
            return from Match match in matches select match.Value;
        }

        /// <summary>
        /// 是否电子邮件
        /// </summary>
        public static bool IsEmail(this string value)
        {
            const string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return value.IsMatch(pattern);
        }

        /// <summary>
        /// 是否是IP地址
        /// </summary>
        public static bool IsIpAddress(this string value)
        {
            const string pattern = @"^(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5])$";
            return value.IsMatch(pattern);
        }

        /// <summary>
        /// 是否是整数
        /// </summary>
        public static bool IsNumeric(this string value)
        {
            const string pattern = @"^\-?[0-9]+$";
            return value.IsMatch(pattern);
        }

        /// <summary>
        /// 是否是Unicode字符串
        /// </summary>
        public static bool IsUnicode(this string value)
        {
            const string pattern = @"^[\u4E00-\u9FA5\uE815-\uFA29]+$";
            return value.IsMatch(pattern);
        }

        /// <summary>
        /// 是否Url字符串
        /// </summary>
        public static bool IsUrl(this string value)
        {
            const string pattern = @"^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*$";
            return value.IsMatch(pattern);
        }

        /// <summary>
        /// 是否身份证号，验证如下3种情况：
        /// 1.身份证号码为15位数字；
        /// 2.身份证号码为18位数字；
        /// 3.身份证号码为17位数字+1个字母
        /// </summary>
        public static bool IsIdentityCard(this string value)
        {
            const string pattern = @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$";
            return value.IsMatch(pattern);
        }

        /// <summary>
        /// 是否手机号码
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isRestrict">是否按严格格式验证</param>
        public static bool IsMobileNumber(this string value, bool isRestrict = false)
        {
            string pattern = isRestrict ? @"^[1][3-8]\d{9}$" : @"^[1]\d{10}$";
            return value.IsMatch(pattern);
        }

        #endregion 正则表达式

        #region 通用方法集合

        /// <summary>
        /// 检查指定的字符串是否为 null 或 string.Empty。
        /// <para>简化 string.IsNullOrEmpty(string) 方法调用。</para>
        /// <para>调用前无需检查字符串实例是否为 null。</para>
        /// <author>sol</author>
        /// </summary>
        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        /// <summary>
        /// 检查指定的字符串在移除前后空白字符后，是否为 null 或 string.Empty。
        /// <para>简化 (string).Trim() + string.IsNullOrEmpty(string) 方法调用。</para>
        /// <para>调用前无需检查字符串实例是否为 null。</para>
        /// <author>sol</author>
        /// </summary>
        public static bool IsTrimmedEmpty(this string text)
        {
            if (text == null)
            {
                return true;
            }
            return IsEmpty(text.Trim());
        }

        /// <summary>
        /// 检查指定的字符串在移除前后空白字符后，是否为 null 或 string.Empty。
        /// <para>简化 (string).Trim(params char[]) + string.IsNullOrEmpty(string) 方法调用。</para>
        /// <para>调用前无需检查字符串实例是否为 null。</para>
        /// <author>sol</author>
        /// </summary>
        public static bool IsTrimmedEmpty(this string text, params char[] trimChars)
        {
            if (text == null)
            {
                return true;
            }
            return IsEmpty(text.Trim(trimChars));
        }

        /// <summary>
        /// 简化 string.Format(string text, params object[] args) 方法调用。
        /// </summary>
        public static string Format(this string text, params object[] args)
        {
            return string.Format(text, args);
        }

        /// <summary>
        /// 验证字符串 <paramref name="text"/> 长度是否在指定范围 (<paramref name="min"/>, <paramref name="max"/>) 内，左右边界均为不包含。
        /// <author>sol</author>
        /// </summary>
        public static bool IsLengthInRange(this string text, int min, int max)
        {
            if (text == null)
            {
                return false;
            }

            int len = text.Length;
            if (min == max)
            {
                return len == min;
            }

            if (min > max)
            {
                int originalMax = max;
                max = min;
                min = originalMax;
            }

            return (len > min && len < max);
        }

        /// <summary>
        /// 验证字符串 <paramref name="text"/> 是否可成功转换为数值类型（Int64/UInt64/double/decimal 任意一种）。
        /// <author>sol</author>
        /// </summary>
        public static bool isNumeric(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            Int64 i64;
            var isInt64 = Int64.TryParse(text, out i64);

            UInt64 u64;
            var isUInt64 = UInt64.TryParse(text, out u64);

            double dbl;
            var isDouble = double.TryParse(text, out dbl);

            decimal dec;
            var isDecimal = decimal.TryParse(text, out dec);

            return (isInt64 || isUInt64 || isDouble || isDecimal);
        }

        /// <summary>
        /// 简化 string.Equals(string, string, StringComparison) 方法调用。
        /// <author>sol</author>
        /// </summary>
        public static bool equalsIgnoreCase(this string text, string otherText)
        {
            return string.Equals(text, otherText, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// （不区分大小写）判断是否包含子串。
        /// <author>sol</author>
        /// </summary>
        public static bool containsIgnoreCase(this string text, string otherText)
        {
            return text != null && otherText != null && text.ToUpper().Contains(otherText.ToUpper());
        }

        /// <summary>
        /// （不区分大小写）判断字符串 <paramref name="text"/> 的开头是否与指定字符串 <paramref name="otherText"/> 匹配。
        /// <author>sol</author>
        /// </summary>
        public static bool startsWithIgnoreCase(this string text, string otherText)
        {
            return text != null && otherText != null && text.ToUpper().StartsWith(otherText.ToUpper());
        }

        /// <summary>
        /// （不区分大小写）判断字符串 <paramref name="text"/> 的结尾是否与指定字符串 <paramref name="otherText"/> 匹配。
        /// <author>sol</author>
        /// </summary>
        public static bool endsWithIgnoreCase(this string text, string otherText)
        {
            return text != null && otherText != null && text.ToUpper().EndsWith(otherText.ToUpper());
        }

        /// <summary>
        /// 简化 string.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries) 方法调用。
        /// <author>sol</author>
        /// </summary>
        public static string[] splitRemoveEmpty(this string text, char separator)
        {
            return text.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion 通用方法集合

        /// <summary>
        /// 返回拼音
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string GetPinyin(this string Str)
        {
            StringBuilder vBuiler = new StringBuilder();
            foreach (char obj in Str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    vBuiler.Append(t.Substring(0, t.Length - 1));
                }
                catch
                {
                    vBuiler.Append(obj.ToString());
                }
            }
            return vBuiler.ToString();
        }

        /// <summary>
        /// 对象序列化String
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 组合字符 串
        /// </summary>
        /// <param name="GetList"></param>
        /// <param name="BuilderObj"></param>
        /// <returns></returns>
        public static string BuilderStringByList(this IEnumerable<string> GetList,string BuilderObj=",")
        {
            StringBuilder vBuilder = new StringBuilder();
            if (GetList != null)
            {
                foreach (var GObj in GetList.Distinct())
                {
                    vBuilder.Append(GObj);
                    vBuilder.Append(BuilderObj);
                }

                if (vBuilder.Length > 0)
                {
                    vBuilder = vBuilder.Remove(vBuilder.Length - 1, 1);
                }
            }

            return vBuilder.ToString();
        }

        /// <summary>
        /// 分割字符串对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <param name="SplitKey"></param>
        /// <returns></returns>
        public static List<T> StringToEnumerable<T>(this string objs, char SplitKey)
        {
            List<T> CallBack = new List<T>();
            var OList = objs.Split(SplitKey);
            foreach (var Obj in OList)
            {
                try
                {
                    T GetObj = (T)Convert.ChangeType(Obj, typeof(T));
                    CallBack.Add(GetObj);
                }
                catch
                {

                }

            }
            return CallBack;
        }

        /// <summary>
        /// 字符串转GUIDS
        /// </summary>
        /// <param name="objs"></param>
        /// <param name="SplitKey"></param>
        /// <returns></returns>
        public static List<Guid> StringToGuidList(this string objs, char SplitKey)
        {
            if (string.IsNullOrEmpty(objs))
            {
                return new List<Guid>();
            }

            List<Guid> CallBack = new List<Guid>();
            var OList = objs.Split(SplitKey);
            foreach (var Obj in OList)
            {
                try
                {
                    Guid TUID = Guid.Empty;
                    if (Guid.TryParse(Obj, out TUID))
                    {
                        CallBack.Add(TUID);
                    }
                }
                catch
                {

                }
            }
            return CallBack;
        }

        /// <summary>
        /// 列表对象加条件及指定分割对象转字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vObjs"></param>
        /// <param name="TFunc"></param>
        /// <param name="SplitKey"></param>
        /// <returns></returns>
        public static string EnumerableToString<T>(this List<T> vObjs, Func<T, string> TFunc, string SplitKey)
        {
            StringBuilder vBuilder = new StringBuilder();
            try
            {
                var GetList = vObjs.Select(TFunc).ToList();
                foreach (var GetObj in GetList)
                {
                    vBuilder.Append(GetObj);
                    vBuilder.Append(SplitKey);
                }

                if (vBuilder.Length > 0)
                {
                    vBuilder = vBuilder.Remove(vBuilder.Length - 1, 1);
                }
            }
            catch (Exception Erro)
            {
                Console.WriteLine("EnumerableToString Error:{0}", Erro.Message);
            }
            return vBuilder.ToString();
        }

        /// <summary>
        /// 检查指定对象是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool CheckIsNullOrEmpty(this string obj)
        {
            if (obj == null)
            {
                return false;
            }

            return !string.IsNullOrEmpty(obj.Trim());
        }
    }
}
