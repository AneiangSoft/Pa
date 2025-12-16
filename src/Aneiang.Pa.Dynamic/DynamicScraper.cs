using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aneiang.Pa.Core.Data;
using Aneiang.Pa.Core.News.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;

namespace Aneiang.Pa.Dynamic
{
    /// <summary>
    /// 微博热门爬虫
    /// </summary>
    public class DynamicScraper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 爬虫
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public DynamicScraper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 获取热门消息
        /// </summary>
        public async Task Scraper()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36");
                client.DefaultRequestHeaders.Referrer = new Uri("https://36kr.com");
                var html = await client.GetStringAsync("https://36kr.com/hot-list/renqi/2025-12-16/1");
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                var rows = htmlDocument.DocumentNode.SelectNodes("//*article-wrapper");
            }
            catch (Exception e)
            {
            }
        }
    }
}
