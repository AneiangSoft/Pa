using Aneiang.Pa.Core.News;
using Aneiang.Pa.News;
using Aneiang.Pa.WeiBo.News;
using Aneiang.Pa.ZhiHu.News;
using Microsoft.Extensions.DependencyInjection;

namespace Aneiang.Pa.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册微博爬取器
        /// </summary>
        /// <param name="services"></param>
        public static void AddNewsScraper(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IWeiBoNewScraper, WeiBoNewScraper>();
            services.AddSingleton<IZhiHuNewScraper, ZhiHuNewScraper>();

            services.AddSingleton<INewsScraper>(provider => provider.GetRequiredService<IWeiBoNewScraper>());
            services.AddSingleton<INewsScraper>(provider => provider.GetRequiredService<IZhiHuNewScraper>());
            services.AddSingleton<INewsScraperFactory, NewsScraperFactory>();
        }
    }
}
