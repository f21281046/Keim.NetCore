using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharp
{
    public class RequestDTO
    {
        /// <summary>
        /// 数据内容
        /// </summary>
        public object Data
        { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string HostAddress
        { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        public string EndPoint
        { get; set; }

        /// <summary>
        /// 请求类型
        /// </summary>
        public Method MethodType
        { get; set; } = Method.POST;

        /// <summary>
        /// 是否需要认证
        /// </summary>
        public bool IsToken
        { get; set; } = false;

    }
}
