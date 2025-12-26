using Aneiang.Pa.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Aneiang.Pa.Dynamic.Extensions
{
    /// <summary>
    /// The service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册爬取器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="httpConfigureHandler"></param>
        public static IServiceCollection AddDynamicScraper(this IServiceCollection services, Func<HttpMessageHandler>? httpConfigureHandler = null)
        {
            services.AddScraper<IDynamicScraper, DynamicScraper>(httpConfigureHandler);
            return services;
        }
    }
}
