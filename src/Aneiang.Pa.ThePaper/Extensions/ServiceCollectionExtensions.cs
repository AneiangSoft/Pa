using Aneiang.Pa.ThePaper.Models;
using Aneiang.Pa.ThePaper.News;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aneiang.Pa.ThePaper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册爬虫
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddThePaperScraper(this IServiceCollection services, IConfiguration? configuration = null)
        {
            if (configuration != null)
            {
                services.Configure<ThePaperScraperOptions>(configuration.GetSection("Scraper:ThePaper"));
            }
            services.AddHttpClient();
            services.AddSingleton<IThePaperNewScraper, ThePaperNewScraper>();
        }
    }
}
