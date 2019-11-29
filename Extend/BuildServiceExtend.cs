using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Keim.NetCore.Extend
{
    public static class BuildServiceExtend
    {
        public static void BuildService<T,V>(this IServiceCollection vService)
        {

            Assembly[] GetAssemblys = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly GetValue in GetAssemblys)
            {
                var GetBaseClass = GetValue.GetTypes().Where(w => w.IsClass && w.BaseType == typeof(T)).ToList();
                foreach (Type MType in GetBaseClass)
                {
                    if (MType.BaseType == typeof(T))
                    {
                        var InterfaceType = MType.GetInterfaces().Where(w => w.GetInterface(typeof(V).Name) != null).FirstOrDefault();
                        if (InterfaceType != null)
                        {
                            vService.AddTransient(InterfaceType, MType);
                        }
                    }
                }
            }
        }
    }
}
