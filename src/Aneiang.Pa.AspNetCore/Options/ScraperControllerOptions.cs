using Aneiang.Pa.AspNetCore.Constants;

namespace Aneiang.Pa.AspNetCore.Options
{
    /// <summary>
    /// 爬虫控制器配置选项
    /// </summary>
    public class ScraperControllerOptions
    {
        /// <summary>
        /// 路由前缀，默认为 "api/scraper"
        /// </summary>
        public string RoutePrefix { get; set; } = ScraperControllerConstants.DefaultRoutePrefix;

        /// <summary>
        /// 是否在路由中使用小写，默认为 true（如：/api/scraper/weibo）
        /// </summary>
        public bool UseLowercaseInRoute { get; set; } = true;

        /// <summary>
        /// 是否启用响应缓存，默认为 false
        /// </summary>
        public bool EnableResponseCaching { get; set; } = false;

        /// <summary>
        /// 响应缓存时长（秒），默认为 300 秒（5分钟）
        /// </summary>
        public int CacheDurationSeconds { get; set; } = 300;
    }
}

