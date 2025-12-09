// Program.cs

using Aneiang.Pa.Core.Data;
using Aneiang.Pa.Core.News;
using Aneiang.Pa.Data;
using Aneiang.Pa.Extensions;
using Aneiang.Pa.News;
using Aneiang.Pa.WeiBo.Extensions;
using Aneiang.Pa.ZhiHu.News;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddNewsScraper();
    })
    .Build();

// 使用服务
using (var scope = builder.Services.CreateScope())
{
    var newsScraperFactory = scope.ServiceProvider.GetRequiredService<INewsScraperFactory>();
    var newsScraper = newsScraperFactory.GetScraper(NewsSource.ZhiHu);
    var newsResult = await newsScraper.GetNewsAsync();
    foreach (var news in newsResult.Data)
    {
        Console.WriteLine($"标题: {news.Title}");
        Console.WriteLine($"URL: {news.Url}");
        if (!string.IsNullOrWhiteSpace(news.ExtensionData))
        {
            Console.WriteLine($"扩展: {news.ExtensionData}");
        }
        Console.WriteLine();
    }
}