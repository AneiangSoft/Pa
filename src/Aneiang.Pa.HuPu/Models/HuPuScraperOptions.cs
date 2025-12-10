using Aneiang.Pa.Core.News;

namespace Aneiang.Pa.HuPu.Models
{
    public class HuPuScraperOptions: ScraperOptions
    {

        public HuPuScraperOptions()
        {
            BaseUrl = "https://bbs.hupu.com";
            NewsUrl = "/topic-daily-hot";
            UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36";
        }
    }
}
