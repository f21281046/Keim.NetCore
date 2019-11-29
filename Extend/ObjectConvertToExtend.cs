using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ObjectConvertTo<T>(this object obj) where T:new()
        {
            Type vType = obj.GetType();
            Type MType = typeof(T);
            object MObj = Activator.CreateInstance(MType);
            var vProperties = vType.GetProperties();
            var MProperties = MType.GetProperties();
            foreach (var Proper in vProperties)
            {

                var IsMproper = MProperties.Where(w => w.Name == Proper.Name).FirstOrDefault();
                if (IsMproper != null)
                {
                    var GetValue = Proper.GetValue(obj, null);
                    try
                    {
                        IsMproper.SetValue(MObj, Convert.ChangeType(GetValue, IsMproper.PropertyType), null);
                    }
                    catch
                    {
                        IsMproper.SetValue(MObj, GetValue);
                    }
                }
            }

            return (T)MObj;
        }

        /// <summary>
        /// 获得特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetObjectAttribute<T>(this object obj) where T:class
        {
            if (obj != null)
            {
                MemberInfo[] mi = obj.GetType().GetMember(obj.ToString());
                if (mi != null && mi.Length > 0)
                {
                    T attr = Attribute.GetCustomAttribute(mi[0], typeof(T)) as T;

                    if (attr != null)
                    {
                        return attr;
                    }
                }
            }
            return null;
        }
    }
}
