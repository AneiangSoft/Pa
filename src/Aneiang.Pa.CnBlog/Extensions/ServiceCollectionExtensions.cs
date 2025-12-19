using Aneiang.Pa.CnBlog.Models;
using Aneiang.Pa.CnBlog.News;
using Aneiang.Pa.Dynamic.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aneiang.Pa.CnBlog.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册爬虫
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCnBlogScraper(this IServiceCollection services, IConfiguration? configuration = null)
        {
            if (configuration != null)
            {
                services.Configure<CnBlogScraperOptions>(configuration.GetSection("Scraper:CnBlog"));
            }
            services.AddHttpClient();
            services.AddDynamicScraper();
            services.AddSingleton<ICnBlogNewScraper, CnBlogNewScraper>();
        }
    }
}
