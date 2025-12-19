<p align="center">
    <img src="assets/logo.png" alt="Aneiang.Pa" width="600" style="vertical-align:middle;border-radius:8px;">
</p>

[中文](README.md) | English

[![NuGet](https://img.shields.io/nuget/v/Aneiang.Pa.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/Aneiang.Pa)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Aneiang.Pa.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/Aneiang.Pa)
[![Target](https://img.shields.io/badge/target-netstandard2.1-blue?style=flat-square)](#)
[![Status](https://img.shields.io/badge/status-active-success?style=flat-square)](#)

A multi-platform trending news/hotlist web scraping library based on .NET. Currently supports crawlers for Weibo, Zhihu, Bilibili, Baidu, Douyin, Hupu, Toutiao, Tencent, Juejin, The Paper, Phoenix News, Douban, and other platforms, complete with demo examples. Open-source project, with plans to add more platforms in the future.

**⚠️ Recommended scraping interval: 5 minutes or more to avoid IP blocking due to frequent requests.**

**⚠️ The scraped data is strictly for personal learning, research, or public welfare purposes only. Commercial sale, attacks on others, or any illegal activities are prohibited. Users assume full legal responsibility for any misuse.**

## Installation (NuGet)
Recommended aggregate package (includes all platforms):
```bash
dotnet add package Aneiang.Pa
```
Install individual packages as needed (example):
```bash
dotnet add package Aneiang.Pa.BaiDu
```

### Available Packages
| Package | Description |
| --- | --- |
| Aneiang.Pa | Aggregate package containing all platform implementations |
| Aneiang.Pa.Core | Core interfaces and models |
| Aneiang.Pa.Dynamic | Dynamic web scraper |
| Aneiang.Pa.BaiDu | Baidu Hotlist scraper |
| Aneiang.Pa.Bilibili | Bilibili Hot Search scraper |
| Aneiang.Pa.WeiBo | Weibo Hot Search scraper |
| Aneiang.Pa.ZhiHu | Zhihu Hotlist scraper |
| Aneiang.Pa.DouYin | Douyin Hotlist scraper |
| Aneiang.Pa.HuPu | Hupu Hot Posts/Hotlist scraper |
| Aneiang.Pa.TouTiao | Toutiao Hotlist scraper |
| Aneiang.Pa.Tencent | Tencent Hotlist scraper |
| Aneiang.Pa.JueJin | Juejin Hotlist scraper |
| Aneiang.Pa.ThePaper | The Paper Hotlist scraper |
| Aneiang.Pa.DouBan | Douban Hotlist scraper |
| Aneiang.Pa.IFeng | Phoenix News Hotlist scraper |
| Aneiang.Pa.Csdn | CSDN Hotlist scraper |
| Aneiang.Pa.CnBlog | Cnblogs Hotlist scraper |

## Quick Start (Local Demo)
1) Restore & Build
```bash
dotnet restore
dotnet build test/Aneiang.Pa.Demo/Aneiang.Pa.Demo.csproj
```
2) Run Demo (default scrapes Baidu Hotlist, modifiable via `ScraperSource`)
```bash
dotnet run --project test/Aneiang.Pa.Demo
```

## Usage in Your Project (NuGet)
```csharp

// Choose one of the following approaches:
// Automatically register all platform scrapers
services.AddNewsScraper();

// Register a single platform scraper
services.AddBaiDuScraper();
```

```csharp
// Get scraper instance via factory pattern
var factory = scope.ServiceProvider.GetRequiredService<INewsScraperFactory>();
var scraper = factory.GetScraper(ScraperSource.BaiDu);
var result = await scraper.GetNewsAsync();

// Directly inject single platform scraper
var scraper = scope.ServiceProvider.GetRequiredService<IBaiDuNewScraper>();
var result = await scraper.GetNewsAsync();
```

## ✨ Advanced Usage - Dynamic Scraping (Aneiang.Pa.Dynamic)

**In addition to basic hot data scraping, we also provide a more flexible, lightweight, and independent scraping library - Aneiang.Pa.Dynamic, capable of scraping data collections from any website.**

### Add NuGet Package
```bash
dotnet add package Aneiang.Pa.Dynamic
```
Usage is achieved by defining model attributes. Example: Scraping [Cnblogs](https://www.cnblogs.com/pick/) hot data:

```csharp
services.AddDynamicScraper();
```

```csharp
var scraperFactory = scope.ServiceProvider.GetRequiredService<IDynamicScraper>();
var testDataSets = await scraperFactory.DatasetScraper<CnBlogOriginalResult>("https://www.cnblogs.com/pick");

```
The key is defining the `CnBlogOriginalResult` model:

```csharp
[HtmlContainer("div", htmlClass: "post-list", htmlId: "post_list", index: 1)]
[HtmlItem("article", htmlClass: "post-item")]
public class CnBlogOriginalResult
{
    [HtmlValue("a", htmlClass: "post-item-title")]
    public string Title { get; set; }

    [HtmlValue(".", attribute: "data-post-id")]
    public string Id { get; set; }

    [HtmlValue("a", htmlClass: "post-item-title", attribute: "href")]
    public string Url { get; set; }

    [HtmlValue(htmlXPath: ".//a[@class=\"post-item-author\"]/span")]
    public string AuthorName { get; set; }

    [HtmlValue("a", htmlClass: "post-item-author", attribute: "href")]
    public string AuthorUrl { get; set; }

    [HtmlValue("p", htmlClass: "post-item-summary")]
    public string Desc { get; set; }

    [HtmlValue(htmlXPath: ".//footer[@class=\"post-item-foot\"]/span[1]")]
    public string CreateTime { get; set; }

    [HtmlValue(htmlXPath: ".//footer[@class=\"post-item-foot\"]/a[2]")]
    public string CommentCount { get; set; }

    [HtmlValue(htmlXPath: ".//footer[@class=\"post-item-foot\"]/a[3]")]
    public string LikeCount { get; set; }

    [HtmlValue(htmlXPath: ".//footer[@class=\"post-item-foot\"]/a[4]")]
    public string ReadCount { get; set; }
}
```
Partial HTML code of the scraped Cnblogs page:
```html
<div id="post_list" class="post-list">
    <article class="post-item" data-post-id="19326078">
        <section class="post-item-body">

            <div class="post-item-text">
                <a class="post-item-title" href="https://www.cnblogs.com/ydswin/p/19326078"
                    target="_blank">Keepalived详解：原理、编译安装与高可用集群配置</a>
                <p class="post-item-summary">
                    <a href="https://www.cnblogs.com/ydswin" target="_blank">
                        <img src="https://pic.cnblogs.com/face/1307305/20240510180945.png" class="avatar" alt="博主头像" />
                    </a>
                    在高可用架构中，避免单点故障至关重要。Keepalived正是为了解决这一问题而生的轻量级工具。本文将深入浅出地介绍Keepalived的工作原理，并提供从编译安装到实战配置的完整指南。
                    1. Keepalived简介与工作原理 Keepalived是一个基于VRRP协议（虚拟路由冗余协议） 实现的 ...
                </p>
            </div>
            <footer class="post-item-foot">
                <a href="https://www.cnblogs.com/ydswin" class="post-item-author"
                    target="_blank"><span>dashery</span></a>

                <span class="post-meta-item">
                <span>2025-12-09 13:01</span>
                </span>
                <a class="post-meta-item btn"
                    href="https://www.cnblogs.com/ydswin/p/19326078#commentform" title="评论 1">
                    <svg width="16" height="16" xmlns="http://www.w3.org/2000/svg">
                        <use xlink:href="#icon_comment"></use>
                    </svg>
                    <span>1</span>
                </a>
                <a id="digg_control_19326078" title="推荐 7" class="post-meta-item btn "
                    href="javascript:void(0)"
                    onclick="DiggPost('ydswin', 19326078, 817406, 1);return false;">
                    <svg width="16" height="16" viewBox="0 0 16 16"
                        xmlns="http://www.w3.org/2000/svg">
                        <use xlink:href="#icon_digg"></use>
                    </svg>
                    <span id="digg_count_19326078">7</span>
                </a>
                <a class="post-meta-item btn" href="https://www.cnblogs.com/ydswin/p/19326078"
                    title="阅读 1892">
                    <svg width="16" height="16" viewBox="0 0 16 16"
                        xmlns="http://www.w3.org/2000/svg">
                        <use xlink:href="#icon_views"></use>
                    </svg>
                    <span>1892</span>
                </a>
                <span id="digg_tip_19326078" class="digg-tip" style="color: red"></span>
            </footer>

        </section>
        <figure>
        </figure>
    </article>
    <!-- Multiple <article> elements -->
</div>
```
### Attribute Description

- `HtmlContainerAttribute`: Dataset container attribute, representing the parent tag containing dataset items. Does not need to be the direct parent. Supports lookup via `id` or `class`. When uniqueness cannot be determined by `id`/`class`, set `index` to retrieve the specified HTML node.
- `HtmlItemAttribute`: Data item attribute, representing the HTML tag attributes for each data record. Supports lookup via `id` or `class`. When uniqueness cannot be determined by `id`/`class`, set `index` to retrieve the specified HTML node.
- `HtmlValueAttribute`: Data value attribute, representing the HTML tag attributes for each field in each data record. Supports lookup via `id` or `class`. When uniqueness cannot be determined by `id`/`class`, set `index` to retrieve the specified HTML node. The `htmlAttribute` field specifies which HTML attribute to extract the value from.

**PS: All three attributes support XPath for HTML tag retrieval. When `HTMLXPath` is not empty, other properties are ignored.**

#### HtmlTag Parameter Explanation

`HtmlTag` and `HTMLXPath` are developed based on `XPath` rules. For more information, refer to `XPath` documentation.

| Selector | Matching Structure | Example |
| --- | --- | --- |
| `p/b` | p directly contains b | `<p><b></b></p>` |
| `p//b` | b within any descendant of p | `<p><div><b></b></div></p>` |
| `p/div/b` | a > div > img | `<p><div><b></b></div></p>` |
| `.` | Only for `HtmlValue`, represents taking the HtmlTag of the current `HtmlItem` | |

### Scraping Result Screenshot
![](./assets/ScreenShot_D.png)

## Roadmap
- ✅ Weibo, Zhihu, Bilibili, Baidu, Douyin, Hupu, Toutiao, Tencent, Juejin, The Paper, Phoenix News, Douban hotlists
- 🚧 Planned: More platforms like GitHub, Steam
- 🧪 Considering: Data scraping needs beyond trending news

## Contributing
- PRs and Issues are welcome, especially for new platform crawlers, improved parsing, and robustness
- Please maintain consistent code style and include brief descriptions and necessary tests before submission
- If you wish to publish your new platform in the NuGet package, please discuss the approach in an Issue first

## License
Aneiang.Pa is licensed under the [MIT License](LICENSE).
