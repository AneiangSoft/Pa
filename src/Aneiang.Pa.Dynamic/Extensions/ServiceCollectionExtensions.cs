using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aneiang.Pa.Dynamic.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册爬取器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDynamicScraper(this IServiceCollection services, IConfiguration? configuration = null)
        {
            services.AddHttpClient();
            services.AddSingleton<IDynamicScraper, DynamicScraper>();
        }
    }
}
