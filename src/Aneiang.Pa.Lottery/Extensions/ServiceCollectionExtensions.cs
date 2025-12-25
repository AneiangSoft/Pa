using Aneiang.Pa.Core.Data;
using Aneiang.Pa.Lottery.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;

namespace Aneiang.Pa.Lottery.Extensions
{
    /// <summary>
    /// The service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册爬虫
        /// </summary>
        /// <param name="services"></param>
        /// <param name="httpConfigureHandler"></param>
        public static void AddLotteryScraper(this IServiceCollection services, Func<HttpMessageHandler>? httpConfigureHandler = null)
        {
            var httpClientBuilder = services.AddHttpClient(PaConsts.DefaultHttpClientName);
            if (httpConfigureHandler != null)
            {
                httpClientBuilder.ConfigurePrimaryHttpMessageHandler(httpConfigureHandler);
            }
            services.TryAddSingleton<ILotteryScraper, LotteryScraper>();
        }
    }
}
