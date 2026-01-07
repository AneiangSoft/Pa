using System;
using Aneiang.Pa.AspNetCore.Constants;

namespace Aneiang.Pa.AspNetCore.Options
{
    /// <summary>
    /// 缓存提供方
    /// </summary>
    public enum ScraperCacheProvider
    {
        /// <summary>
        /// 不启用缓存
        /// </summary>
        None = 0,

        /// <summary>
        /// 进程内内存缓存
        /// </summary>
        Memory = 1,

        /// <summary>
        /// Redis 分布式缓存
        /// </summary>
        Redis = 2,
    }

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
        /// 缓存提供方，默认 None（不缓存）
        /// </summary>
        public ScraperCacheProvider CacheProvider { get; set; } = ScraperCacheProvider.None;

        /// <summary>
        /// 默认缓存时长，默认 1 小时
        /// </summary>
        public TimeSpan CacheDuration { get; set; } = TimeSpan.FromHours(1);

        /// <summary>
        /// Redis 配置（当 CacheProvider=Redis 时生效）
        /// </summary>
        public ScraperRedisCacheOptions Redis { get; set; } = new ScraperRedisCacheOptions();
    }

    /// <summary>
    /// Redis 缓存配置
    /// </summary>
    public class ScraperRedisCacheOptions
    {
        /// <summary>
        /// Redis 连接字符串（如：localhost:6379）
        /// </summary>
        public string? Configuration { get; set; }

        /// <summary>
        /// Redis 实例名前缀（可选，用于区分不同应用的 Key）
        /// </summary>
        public string? InstanceName { get; set; }
    }
}

