using Aneiang.Pa.Core.News;
using Aneiang.Pa.Core.News.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Aneiang.Pa.Core.Data;
using HtmlAgilityPack;

namespace Aneiang.Pa.WeiBo.News
{
    /// <summary>
    /// 微博热门
    /// </summary>
    public class WeiBoNewScraper : IWeiBoNewScraper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 微博热门
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public WeiBoNewScraper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 爬取地址
        /// </summary>
        public string BaseUrl => "https://s.weibo.com/top/summary?cate=realtimehot";

        /// <summary>
        /// 标识
        /// </summary>
        public string Source => "WeiBo";

        /// <summary>
        /// 获取热门消息
        /// </summary>

        public async Task<NewsResult> GetNewsAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Cookie", "SUB=_2AkMWIuNSf8NxqwJRmP8dy2rhaoV2ygrEieKgfhKJJRMxHRl-yT9jqk86tRB6PaLNvQZR6zYUcYVT1zSjoSreQHidcUq7");
            client.DefaultRequestHeaders.Referrer = new Uri(BaseUrl);
            var html = await client.GetStringAsync(BaseUrl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var rows = htmlDocument.DocumentNode.SelectNodes("//*[@id='pl_top_realtimehot']//table/tbody/tr");
            var newsResult = new NewsResult();
            if (rows == null) return newsResult;

            // 跳过第一行（可能是表头）
            for (int i = 1; i < rows.Count; i++)
            {
                var row = rows[i];
                var linkNode = row.SelectSingleNode(".//td[@class='td-02']/a[not(contains(@href, 'javascript:void(0);'))]");

                if (linkNode != null)
                {
                    var title = linkNode.InnerText?.Trim();
                    var href = linkNode.GetAttributeValue("href", "");

                    if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(href))
                    {
                        var flagNode = row.SelectSingleNode(".//td[@class='td-03']");
                        var flag = flagNode?.InnerText?.Trim();

                        string flagUrl = null;
                        if (flag == "新")
                        {
                            flagUrl = "https://simg.s.weibo.com/moter/flags/1_0.png";
                        }
                        else if (flag == "热")
                        {
                            flagUrl = "https://simg.s.weibo.com/moter/flags/2_0.png";
                        }

                        var newsItem = new NewsItem
                        {
                            Id = title,
                            Title = title,
                            Url = $"{BaseUrl}{href}",
                            MobileUrl = $"{BaseUrl}{href}"
                        };
                        newsItem.SetProperty("Icon", flagUrl);
                        newsResult.Data.Add(newsItem);
                    }
                }
            }

            return newsResult;
        }
    }
}
