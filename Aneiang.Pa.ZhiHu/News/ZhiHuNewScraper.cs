using Aneiang.Pa.Core.Data;
using Aneiang.Pa.Core.News;
using Aneiang.Pa.Core.News.Models;
using Aneiang.Pa.ZhiHu.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aneiang.Pa.ZhiHu.News
{
    /// <summary>
    /// 微博热门
    /// </summary>
    public class ZhiHuNewScraper : IZhiHuNewScraper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 微博热门
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public ZhiHuNewScraper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 爬取地址
        /// </summary>
        //public string BaseUrl => "https://www.zhihu.com/api/v3/feed/topstory/hot-list-web?limit=20&desktop=true";

        public string BaseUrl => "http://127.0.0.1:5500/hot-list-web.json";

        /// <summary>
        /// 标识
        /// </summary>
        public string Source => "ZhiHu";


        /// <summary>
        /// 获取热门消息
        /// </summary>

        public async Task<NewsResult> GetNewsAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Referrer = new Uri(BaseUrl);
            var newsResult = new NewsResult();
            var response = await client.GetAsync(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ZhiHuOriginalResult>(jsonString);
                if (result == null) return newsResult;
                foreach (var item in result.data)
                {
                    var newsItem = new NewsItem 
                    {
                        Id = item.card_id,
                        Title = item.target.title_area.text,
                        Url = item.target.link.url,
                        MobileUrl = item.target.link.url,
                    };
                    newsItem.SetProperty("Desc", item.target.excerpt_area);
                    newsItem.SetProperty("ImageUrl", item.target.image_area.url);
                    newsItem.SetProperty("AnswerCount", item.feed_specific.answer_count);
                    newsItem.SetProperty("Metrics", item.target.metrics_area.text);
                    newsResult.Data.Add(newsItem);
                }
            }
            return newsResult;
        }
    }
}
