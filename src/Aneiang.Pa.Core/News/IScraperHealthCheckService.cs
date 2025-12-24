using System.Threading.Tasks;
using Aneiang.Pa.Core.News.Models;

namespace Aneiang.Pa.Core.News
{
    /// <summary>
    /// 爬虫健康检查服务接口
    /// </summary>
    public interface IScraperHealthCheckService
    {
        /// <summary>
        /// 检查所有爬虫的健康状态
        /// </summary>
        /// <param name="timeoutMs">超时时间（毫秒），默认5000</param>
        /// <returns>健康检查结果</returns>
        Task<HealthCheckResult> CheckAllAsync(int timeoutMs = 5000);

        /// <summary>
        /// 检查指定爬虫的健康状态
        /// </summary>
        /// <param name="scraper">爬虫实例</param>
        /// <param name="timeoutMs">超时时间（毫秒），默认5000</param>
        /// <returns>爬虫健康状态</returns>
        Task<ScraperHealthStatus> CheckAsync(INewsScraper scraper, int timeoutMs = 5000);
    }
}
