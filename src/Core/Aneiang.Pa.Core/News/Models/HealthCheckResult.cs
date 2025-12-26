using System;
using System.Collections.Generic;

namespace Aneiang.Pa.Core.News.Models
{
    /// <summary>
    /// 单个爬虫健康检查结果
    /// </summary>
    public class ScraperHealthStatus
    {
        /// <summary>
        /// 爬虫源标识
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 是否健康
        /// </summary>
        public bool IsHealthy { get; set; }

        /// <summary>
        /// 响应时间（毫秒）
        /// </summary>
        public long ResponseTimeMs { get; set; }

        /// <summary>
        /// 错误消息（如果有）
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime CheckTime { get; set; }
    }

    /// <summary>
    /// 健康检查结果
    /// </summary>
    public class HealthCheckResult
    {
        /// <summary>
        /// 初始化健康检查结果
        /// </summary>
        public HealthCheckResult()
        {
            Scrapers = new List<ScraperHealthStatus>();
        }

        /// <summary>
        /// 总爬虫数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 健康爬虫数
        /// </summary>
        public int HealthyCount { get; set; }

        /// <summary>
        /// 不健康爬虫数
        /// </summary>
        public int UnhealthyCount { get; set; }

        /// <summary>
        /// 整体是否健康（所有爬虫都健康）
        /// </summary>
        public bool IsHealthy => UnhealthyCount == 0 && TotalCount > 0;

        /// <summary>
        /// 各爬虫的健康状态
        /// </summary>
        public List<ScraperHealthStatus> Scrapers { get; set; }

        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime CheckTime { get; set; }
    }
}
