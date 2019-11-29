using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp
{
    public static class RestSharpExtend
    {
        /// <summary>
        /// 执行请求并返回结果信息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<TModel> ExecuteAsyncWithErrorThrow<TModel>(this RestClient client, IRestRequest request) where TModel : new()
        {
            IRestResponse<TModel> response = await client.ExecuteTaskAsync<TModel>(request);
            CheckError(response);
            return response.Data;
        }

        public static async Task<string> ExecuteAsyncWithErrorThrow(this RestClient client, IRestRequest request)
        {
            IRestResponse response = await client.ExecuteTaskAsync(request);
            CheckError(response);
            return response.Content;
        }

        public static TModel ExecuteWithErrorThrow<TModel>(this RestClient client, IRestRequest request) where TModel : new()
        {
            IRestResponse<TModel> response = client.Execute<TModel>(request);
            CheckError(response);
            return response.Data;
        }

        public static string ExecuteWithErrorThrow(this RestClient client, IRestRequest request)
        {
            IRestResponse response = client.Execute(request);
            CheckError(response);
            return response.Content;
        }

        public static dynamic ExecuteDynamicWithErrorThrow(this RestClient client, IRestRequest request)
        {
            RestResponse<dynamic> response;
            try
            {
                response = client.ExecuteDynamic(request);
            }
            catch (Exception ex)
            {
                throw new Exception("动态类型转换错误", ex);
            }

            CheckError(response);
            return response.Data;
        }

        public static async Task<dynamic> ExecuteDynamicAsyncWithErrorThrow(this RestClient client, IRestRequest request)
        {
            RestResponse<dynamic> response;
            try
            {
                response = client.ExecuteDynamic(request);
            }
            catch (Exception ex)
            {
                throw new Exception("动态类型转换错误", ex);
            }

            CheckError(response);
            return await Task.FromResult(response.Data);
        }

        private static void CheckError(IRestResponse response)
        {
            Console.WriteLine($"Is Check Error{response.StatusCode.ToString()}");
            if (response.StatusCode == 0)
            {
                if (response.ErrorException != null)
                {
                    throw new Exception(response.ErrorException.ToString());
                }
                else
                {
                    throw new Exception("网络错误!");
                }
            }
            else
            {
                Console.WriteLine("Switch Start");
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                    case System.Net.HttpStatusCode.InternalServerError:
                        throw new Exception(response.ErrorMessage);
                    case System.Net.HttpStatusCode.BadRequest:
                        throw new Exception(response.Content);
                    case System.Net.HttpStatusCode.OK:
                        Console.WriteLine("Is OK");
                        if (response.ErrorException != null)
                        {
                            throw new Exception(response.ErrorMessage, response.ErrorException);
                        }
                        return;
                }
            }
        }
    }
}
