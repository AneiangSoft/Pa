# <img src="assets/icon.jpg" alt="Aneiang.Pa" width="64" height="64" style="vertical-align:middle;border-radius:8px;"> Aneiang.Pa

[![NuGet](https://img.shields.io/nuget/v/Aneiang.Pa.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/Aneiang.Pa)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Aneiang.Pa.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/Aneiang.Pa)
[![Target](https://img.shields.io/badge/target-netstandard2.1-blue?style=flat-square)](#)
[![Status](https://img.shields.io/badge/status-active-success?style=flat-square)](#)

一个基于 .NET 的多平台热门新闻/热榜爬虫库，提供统一接口、工厂与依赖注入支持，当前内置微博、知乎、B 站、百度四个平台实现，并附带 Demo 示例。项目开源，后续将增加更多平台。

## 功能特性
- 🧩 统一接口：所有爬虫实现 `INewsScraper`，返回 `NewsResult` / `NewsItem`
- 🏭 可插拔工厂：通过 `INewsScraperFactory` + `ScraperSource` 获取指定爬虫
- ⚙️ 配置驱动：`Options` 模式，支持自定义 URL/Cookie/User-Agent
- 🗃️ 原始数据保留：`NewsItem.ExtensionData` 可存储原始响应与附加字段
- 🔌 DI 集成：`AddNewsScraper` 一键注册 HttpClient、各平台爬虫与工厂

## 目录结构
```
Aneiang.Pa/
├── src/
│   ├── Aneiang.Pa.Core/        # 核心接口与模型
│   ├── Aneiang.Pa/             # 工厂与扩展方法
│   ├── Aneiang.Pa.BaiDu/       # 百度热榜实现
│   ├── Aneiang.Pa.ZhiHu/       # 知乎热榜实现
│   ├── Aneiang.Pa.Bilibili/    # B 站热搜实现
│   └── Aneiang.Pa.WeiBo/       # 微博热搜实现
└── test/
    └── Aneiang.Pa.Demo/        # 演示项目
```

## 环境要求
- .NET SDK 8.0（运行 Demo）
- 库目标框架：netstandard2.1（可被 .NET Core 3.1+ / .NET 5+ / .NET 8+ 引用）

## 安装（NuGet）
推荐聚合包（含全部平台）：
```bash
dotnet add package Aneiang.Pa --version 1.0.3
```
按需引用单个包（示例）：
```bash
dotnet add package Aneiang.Pa.WeiBo --version 1.0.3
```

### 已发布包（nuget.org，当前版本 1.0.3）
| Package | 版本 | 说明 |
| --- | --- | --- |
| Aneiang.Pa | 1.0.3 | 聚合包，包含全部平台实现 |
| Aneiang.Pa.Core | 1.0.3 | 核心接口与模型 |
| Aneiang.Pa.BaiDu | 1.0.3 | 百度热榜爬虫 |
| Aneiang.Pa.Bilibili | 1.0.3 | B 站热搜爬虫 |
| Aneiang.Pa.WeiBo | 1.0.3 | 微博热搜爬虫 |
| Aneiang.Pa.ZhiHu | 1.0.3 | 知乎热榜爬虫 |

## 快速开始（本地 Demo）
1) 还原 & 构建
```bash
dotnet restore
dotnet build test/Aneiang.Pa.Demo/Aneiang.Pa.Demo.csproj
```

2) 配置 `test/Aneiang.Pa.Demo/appsettings.json`（示例见下）

3) 运行 Demo（默认抓取百度热榜，可修改 `ScraperSource`）
```bash
dotnet run --project test/Aneiang.Pa.Demo
```

## 在你的项目中使用（NuGet）
```csharp
using Aneiang.Pa.Data;
using Aneiang.Pa.Extensions;
using Aneiang.Pa.News;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(cfg => cfg.AddJsonFile("appsettings.json", optional: true))
    .ConfigureServices((ctx, services) =>
    {
        // 自动注册 HttpClient、各平台爬虫与工厂
        services.AddNewsScraper(ctx.Configuration);
    })
    .Build();

using var scope = host.Services.CreateScope();
var factory = scope.ServiceProvider.GetRequiredService<INewsScraperFactory>();
var scraper = factory.GetScraper(ScraperSource.BaiDu); // WeiBo / ZhiHu / Bilibili
var result = await scraper.GetNewsAsync();
foreach (var news in result.Data)
{
    Console.WriteLine($"{news.Title} - {news.Url}");
}
```

## 配置示例（appsettings.json）
```json
{
  "Scraper": {
    "WeiBo": {
      "BaseUrl": "https://s.weibo.com",
      "Cookie": "替换为你的 Cookie",
      "UserAgent": "Mozilla/5.0 ...",
      "NewsUrl": "/top/summary?cate=realtimehot"
    },
    "ZhiHu": {
      "NewsUrl": "https://www.zhihu.com/api/v3/feed/topstory/hot-list-web?limit=20&desktop=true"
    },
    "Bilibili": {
      "SearchUrl": "https://s.search.bilibili.com/main/hotword?limit=30"
    },
    "BaiDu": {
      "NewsUrl": "https://top.baidu.com/board?tab=realtime"
    }
  }
}
```
注意：SDK默认配置了，通常情况下可以不进行配置；当默认配置失效后，才会用到自定义配置。

## 规划与 Roadmap
- ✅ 微博、知乎、B 站、百度热榜
- 🚧 计划：抖音、头条、Twitter/X 等更多平台
- 🧪 考虑：统一的重试/限流策略、更多元数据字段

## 贡献
- 欢迎 PR / Issue，尤其是新增平台爬虫、改进解析与健壮性
- 提交前请保持代码风格一致，并附带简要说明和必要的测试
- 如果希望在 NuGet 包中发布你新增的平台，请在 Issue 先讨论方案

## 许可证
Aneiang.Pa 采用 [MIT 许可证](LICENSE)。

