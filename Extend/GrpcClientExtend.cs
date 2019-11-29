using Grpc.Core;
using Keim.NetCore.DTO;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keim.NetCore.Extend
{
    public static class GrpcClientExtend
    {
        public static Channel GetChannel(this MicroServiceItem obj, ChannelCredentials credentials = null)
        {
            if (credentials == null) credentials = ChannelCredentials.Insecure;
            return new Channel(obj.ServiceEndPoint, obj.ServicePort, credentials);
        }
    }
}
