using Aneiang.Pa.Core.News.Models;
using Aneiang.Pa.Models;
using Aneiang.Pa.News;
using Microsoft.AspNetCore.Mvc;

namespace Aneiang.Pa.ClientDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NewsController : ControllerBase
    {

        private readonly ILogger<NewsController> _logger;
        private readonly INewsScraperFactory _newsScraperFactory;

        public NewsController(ILogger<NewsController> logger, INewsScraperFactory newsScraperFactory)
        {
            _logger = logger;
            _newsScraperFactory = newsScraperFactory;
        }

        [HttpGet(Name = "GetNews")]
        public async Task<NewsResult> GetNews()
        {
            var newsScraper = _newsScraperFactory.GetScraper(ScraperSource.WeiBo);
            var result = await newsScraper.GetNewsAsync();
            return result;
        }
    }
}
