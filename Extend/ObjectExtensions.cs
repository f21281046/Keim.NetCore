using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static partial class ObjectExtensions
    {
        #region 验证
        /// <summary>
        /// 检测对象是否为null,为null则抛出<see cref="ArgumentNullException"/>异常
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckNull(this object obj, string parameterName)
        {
            if (obj == null)
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// 检测对象是否为null,为null则抛出<see cref="ArgumentNullException"/>异常
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="parameterName">参数名</param>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty<T>(this IEnumerable<T> value)
        {
            if (value == null)
                return true;
            return !value.Any();
        }

        #endregion

        #region 取值
        private static DateTime? _systemTime = null;
        public static DateTime SystemTime
        {
            get { return _systemTime.HasValue ? _systemTime.Value : DateTime.Now; }
            set { _systemTime = value; }
        }

        public static string GetValue(this object o, string def = "")
        {
            if (o != null)
            {
                if (o.ToString().Trim() != "")
                {
                    return o.ToString().Trim();
                }
                else
                {
                    return def;
                }
            }
            else
            {
                return def;
            }
        }

        public static string GetDateTimeValue(this object o, string format = "yyyy/MM/dd")
        {
            if (o != null)
            {
                if (o.ToString().Trim() != "")
                {
                    DateTime v = SystemTime;
                    if (DateTime.TryParse(o.ToString().Trim(), out v))
                    {
                        return v.ToString(format);
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public static string GetNumberValue(this object o, string def = "")
        {
            if (o != null)
            {
                if (o.ToString().Trim() != "")
                {
                    decimal v = 0;
                    if (decimal.TryParse(o.ToString().Trim(), out v))
                    {
                        return v.ToString().Trim();
                    }
                    else
                    {
                        return def;
                    }
                }
                else
                {
                    return def;
                }
            }
            else
            {
                return def;
            }
        }

        public static string GetBooleanValue(this object o, string def = "")
        {
            if (o != null)
            {
                if (o.ToString().Trim() != "")
                {
                    bool v = false;
                    if (bool.TryParse(o.ToString().Trim(), out v))
                    {
                        return v.ToString().Trim();
                    }
                    else
                    {
                        return def;
                    }
                }
                else
                {
                    return def;
                }
            }
            else
            {
                return def;
            }
        }
        #endregion

        /// <summary>
        /// 选择指定项去重复
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// String序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static T DeSerializeJson<T>(this string Json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Json);
        }
    }
}
