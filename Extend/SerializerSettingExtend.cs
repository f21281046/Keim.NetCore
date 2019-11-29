using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SerializerSettingExtend
    {
        public static IMvcBuilder AddSerializerSetting(this IMvcBuilder Mvc, IContractResolver contract=null)
        {
            return Mvc.AddJsonOptions(Options =>
            {
                Options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                Options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                // Options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                if (contract == null) contract = new CamelCasePropertyNamesContractResolver();
                Options.SerializerSettings.ContractResolver = contract;
            });
        }
    }
}
