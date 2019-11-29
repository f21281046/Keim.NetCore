using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharp
{
    public class HttpErrorException : Exception
    {
        /// <summary>
        /// 从 <paramref name="error"/> 还原创建异常实例。
        /// </summary>
        /// <param name="error">HttpError 实例</param>
        /// <param name="statusCode">HTTP 状态代码</param>
        /// <returns></returns>
        public static HttpErrorException Create(Exception error, int statusCode = 0)
        {
            if (error == null)
            {
                return null;
            }

            var result = new HttpErrorException(error.Message, Create(error.InnerException), error.Source, error.StackTrace, statusCode);
            return result;
        }

        public HttpErrorException(string message)
            : base(message)
        {
        }

        public HttpErrorException(string message, Exception innerException, string exceptionType, string stackTrace, int statusCode = 0)
            : base(message, innerException)
        {
            ExceptionType = exceptionType;
            StackTrace = stackTrace;
            StatusCode = statusCode;
        }

        /// <summary>
        /// HTTP 状态代码
        /// </summary>
        public int StatusCode { get; private set; }

        /// <summary>
        /// 来源异常实际类型
        /// </summary>
        public string ExceptionType { get; private set; }

        /// <summary>
        /// 来源异常堆栈跟踪
        /// </summary>
        public new string StackTrace { get; private set; }
    }
}
