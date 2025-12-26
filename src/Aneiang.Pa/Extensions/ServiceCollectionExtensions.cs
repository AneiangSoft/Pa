using System;
using System.Net.Http;
using Aneiang.Pa.Dynamic.Extensions;
using Aneiang.Pa.Lottery.Extensions;
using Aneiang.Pa.News.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aneiang.Pa.Extensions
{
    /// <summary>
    ///     The service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     注册爬取器
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置对象</param>
        /// <param name="httpConfigureHandler">HTTP 消息处理器工厂</param>
        /// <param name="configureHttpClient">HTTP 客户端配置操作</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddPaScraper(
            this IServiceCollection services,
            IConfiguration? configuration = null,
            Func<HttpMessageHandler>? httpConfigureHandler = null,
            Action<IHttpClientBuilder>? configureHttpClient = null)
        {
            services.AddNewsScraper(configuration, httpConfigureHandler, configureHttpClient);
            services.AddDynamicScraper(httpConfigureHandler, false);
            services.AddLotteryScraper(httpConfigureHandler, false);
            return services;
        }
    }
}
