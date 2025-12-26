using Aneiang.Pa.Core.News.Models;
using Aneiang.Pa.Core.Scraper;
using System.Threading.Tasks;

namespace Aneiang.Pa.Core.News
{
    /// <summary>
    /// 新闻爬取器
    /// </summary>
    public interface INewsScraper : IScraper
    {
        /// <summary>
        /// 标识
        /// </summary>
        string Source { get; }

        /// <summary>
        /// 获取新闻
        /// </summary>
        /// <returns></returns>
        Task<AneiangGenericListResult<NewsItem>> GetNewsAsync();
    }
}
