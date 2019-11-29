using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Keim.NetCore.Tools
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RequestHeaderFilter : ActionFilterAttribute
    {
        private static ILogger<RequestHeaderFilter> BizLogger;

     

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var actionName = context.HttpContext.Request.Path.Value;
            var actionHostAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            var actionHostPort = context.HttpContext.Connection.RemotePort.ToString();
            var GetSID = context.HttpContext.TraceIdentifier;
            DateTime STime = DateTime.Now;
            if (BizLogger != null) BizLogger.LogWarning($"Action  Address:{actionHostAddress} Port:{actionHostPort} STime:{STime.ToString()} Action:{actionName}");
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var actionName = context.HttpContext.Request.Path.Value;
            var actionHostAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            var actionHostPort = context.HttpContext.Connection.RemotePort.ToString();
            var GetSID = context.HttpContext.TraceIdentifier;
            DateTime ETime = DateTime.Now;
            if (BizLogger != null) BizLogger.LogWarning($"EndAction  Address:{actionHostAddress} Port:{actionHostPort} ETime:{ETime.ToString()} Action:{actionName}");
        }


    }
}
