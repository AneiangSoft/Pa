using System;

namespace Aneiang.Pa.Core.News
{
    /// <summary>
    /// 爬取器配置选项
    /// </summary>
    public class ScraperOptions
    {
        /// <summary>
        /// Base地址
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// 新闻/热门 地址
        /// </summary>
        public string NewsUrl { get; set; }

        /// <summary>
        /// Cookie
        /// </summary>
        public string Cookie { get; set; }

        /// <summary>
        /// UserAgent
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <exception cref="ArgumentException">当配置参数不完整或缺失时抛出</exception>
        public virtual void Check()
        {
            if (string.IsNullOrWhiteSpace(BaseUrl))
            {
                throw new ArgumentException("BaseUrl 配置参数不能为空", nameof(BaseUrl));
            }

            if (string.IsNullOrWhiteSpace(NewsUrl))
            {
                throw new ArgumentException("NewsUrl 配置参数不能为空", nameof(NewsUrl));
            }

            // 验证 BaseUrl 格式
            if (!Uri.TryCreate(BaseUrl, UriKind.Absolute, out var baseUri))
            {
                throw new ArgumentException($"BaseUrl 格式无效: {BaseUrl}", nameof(BaseUrl));
            }

            // 如果 UserAgent 为空，自动生成一个
            if (string.IsNullOrWhiteSpace(UserAgent))
            {
                UserAgent = UserAgentGenerator.GetRandomUserAgent();
            }
        }
    }
}
