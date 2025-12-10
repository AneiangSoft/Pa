using Aneiang.Pa.HuPu.Models;
using Aneiang.Pa.HuPu.News;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aneiang.Pa.HuPu.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册爬取器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddHuPuScraper(this IServiceCollection services, IConfiguration? configuration = null)
        {
            if (configuration != null)
            {
                services.Configure<HuPuScraperOptions>(configuration.GetSection("Scraper:TouTiao"));
            }
            services.AddHttpClient();
            services.AddSingleton<IHuPuNewScraper, HuPuNewScraper>();
        }
    }
}
