using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder.Model
{
    public class ServiceEntity
    {
        public bool IsNetwork { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string ServiceName { get; set; }
        public string ConsulIP { get; set; }
        public int ConsulPort { get; set; }
    }
}
