using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aneiang.Pa.Dynamic
{
    /// <summary>
    /// 动态爬虫接口
    /// </summary>
    public interface IDynamicScraper
    {
        /// <summary>
        /// 通用数据集抓取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="referer"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        Task<List<T>> DatasetScraper<T>(string url, string? referer = null, string? userAgent = null) where T : new();
    }
}
