using Aneiang.Pa.ZhiHu.News;
using Microsoft.Extensions.DependencyInjection;

namespace Aneiang.Pa.ZhiHu.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册微博爬取器
        /// </summary>
        /// <param name="services"></param>
        public static void AddZhiHuScraper(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IZhiHuNewScraper, ZhiHuNewScraper>();
        }
    }
}
