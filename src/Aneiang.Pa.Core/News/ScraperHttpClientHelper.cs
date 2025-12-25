using Aneiang.Pa.Core.Data;
using Aneiang.Pa.Core.News.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aneiang.Pa.Core.News
{
    /// <summary>
    /// Scraper HTTP 客户端辅助类，用于减少代码重复
    /// </summary>
    public static class ScraperHttpClientHelper
    {
        /// <summary>
        /// 创建并配置 HTTP 客户端
        /// </summary>
        /// <param name="httpClientFactory">HTTP 客户端工厂</param>
        /// <param name="baseUrl">基础 URL</param>
        /// <param name="userAgent">User-Agent</param>
        /// <param name="cookie">Cookie（可选）</param>
        /// <returns>配置好的 HTTP 客户端</returns>
        public static HttpClient CreateConfiguredClient(
            IHttpClientFactory httpClientFactory,
            string baseUrl,
            string userAgent,
            string? cookie = null)
        {
            var client = httpClientFactory.CreateClient(PaConsts.DefaultHttpClientName);
            
            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                client.DefaultRequestHeaders.Referrer = new Uri(baseUrl);
            }
            
            if (!string.IsNullOrWhiteSpace(userAgent))
            {
                client.DefaultRequestHeaders.UserAgent.Clear();
                client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
            }
            
            if (!string.IsNullOrWhiteSpace(cookie))
            {
                client.DefaultRequestHeaders.Remove("Cookie");
                client.DefaultRequestHeaders.Add("Cookie", cookie);
            }
            
            return client;
        }

        /// <summary>
        /// 执行 HTTP GET 请求并返回字符串
        /// </summary>
        /// <param name="client">HTTP 客户端</param>
        /// <param name="url">请求 URL</param>
        /// <returns>响应字符串</returns>
        public static async Task<string> GetStringAsync(HttpClient client, string url)
        {
            return await client.GetStringAsync(url).ConfigureAwait(false);
        }

        /// <summary>
        /// 执行 HTTP GET 请求并返回响应
        /// </summary>
        /// <param name="client">HTTP 客户端</param>
        /// <param name="url">请求 URL</param>
        /// <returns>HTTP 响应消息</returns>
        public static async Task<HttpResponseMessage> GetAsync(HttpClient client, string url)
        {
            return await client.GetAsync(url).ConfigureAwait(false);
        }

        /// <summary>
        /// 创建错误结果
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <param name="source">来源标识</param>
        /// <returns>新闻结果</returns>
        public static AneiangGenericListResult<NewsItem> CreateNewsErrorResult(Exception exception, string source)
        {
            var errorMessage = $"爬取 {source} 数据时发生错误: {exception.Message}";
            if (exception.InnerException != null)
            {
                errorMessage += $" | 内部错误: {exception.InnerException.Message}";
            }
            return new AneiangGenericListResult<NewsItem>(false, errorMessage);
        }
    }
}

