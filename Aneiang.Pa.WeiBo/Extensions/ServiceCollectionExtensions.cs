using Aneiang.Pa.Core.News;
using Aneiang.Pa.WeiBo.News;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aneiang.Pa.WeiBo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册微博爬取器
        /// </summary>
        /// <param name="services"></param>
        public static void AddWeiBoScraper(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IWeiBoNewScraper, WeiBoNewScraper>();
        }
    }
}
