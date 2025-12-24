using System;
using System.Linq;
using System.Threading.Tasks;
using Aneiang.Pa.Core.News;
using Aneiang.Pa.Core.News.Models;
using Aneiang.Pa.Models;
using Aneiang.Pa.News;
using Aneiang.Pa.AspNetCore.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aneiang.Pa.AspNetCore.Controllers
{
    /// <summary>
    /// 爬虫控制器
    /// </summary>
    [ApiController]
    [Route("api/scraper")]
    [Produces("application/json")]
    public class ScraperController : ControllerBase
    {
        private readonly INewsScraperFactory _scraperFactory;
        private readonly ILogger<ScraperController> _logger;
        private readonly ScraperControllerOptions _options;
        private static readonly string[] AvailableSources = System.Enum.GetNames(typeof(ScraperSource));

        /// <summary>
        /// 初始化爬虫控制器
        /// </summary>
        /// <param name="scraperFactory">爬虫工厂</param>
        /// <param name="logger">日志记录器</param>
        /// <param name="options">配置选项</param>
        public ScraperController(
            INewsScraperFactory scraperFactory, 
            ILogger<ScraperController> logger,
            IOptions<ScraperControllerOptions> options)
        {
            _scraperFactory = scraperFactory ?? throw new ArgumentNullException(nameof(scraperFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options?.Value ?? new ScraperControllerOptions();
        }

        /// <summary>
        /// 获取指定平台的新闻
        /// </summary>
        /// <param name="source">爬虫源（支持大小写不敏感）</param>
        /// <returns>新闻结果</returns>
        /// <response code="200">成功获取新闻</response>
        /// <response code="400">请求参数无效</response>
        /// <response code="404">不支持的爬虫源</response>
        /// <response code="500">服务器内部错误</response>
        [HttpGet("{source}")]
        [ProducesResponseType(typeof(NewsResult), 200)]
        [ProducesResponseType(typeof(NewsResult), 400)]
        [ProducesResponseType(typeof(NewsResult), 404)]
        [ProducesResponseType(typeof(NewsResult), 500)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult<NewsResult>> GetNews([FromRoute] string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                _logger.LogWarning("GetNews called with empty source parameter");
                return BadRequest(new NewsResult(false, "源参数不能为空"));
            }

            try
            {
                // 尝试解析枚举值（支持大小写不敏感）
                if (!System.Enum.TryParse<ScraperSource>(source, true, out var scraperSource))
                {
                    _logger.LogWarning("Unsupported scraper source: {Source}", source);
                    return NotFound(new NewsResult(false, $"不支持的爬虫源: {source}"));
                }

                _logger.LogInformation("Fetching news from source: {Source}", scraperSource);
                var scraper = _scraperFactory.GetScraper(scraperSource);
                var result = await scraper.GetNewsAsync();
                
                if (!result.IsSuccessd)
                {
                    _logger.LogError("Failed to fetch news from {Source}: {ErrorMessage}", scraperSource, result.ErrorMessage);
                    return StatusCode(500, result);
                }

                _logger.LogInformation("Successfully fetched {Count} news items from {Source}", result.Data.Count, scraperSource);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "ArgumentException when fetching news from source: {Source}", source);
                return NotFound(new NewsResult(false, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when fetching news from source: {Source}", source);
                return StatusCode(500, new NewsResult(false, $"获取新闻失败: {ex.Message}"));
            }
        }

        /// <summary>
        /// 获取所有支持的爬虫源列表
        /// </summary>
        /// <returns>支持的爬虫源列表</returns>
        /// <response code="200">成功获取爬虫源列表</response>
        [HttpGet]
        [ProducesResponseType(typeof(object), 200)]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)] // 缓存1小时，因为源列表很少变化
        public ActionResult GetAvailableSources()
        {
            return Ok(new 
            { 
                sources = AvailableSources,
                count = AvailableSources.Length
            });
        }
    }
}

