using Aneiang.Pa.AspNetCore.Options;
using Aneiang.Pa.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aneiang.Pa.AspNetCore.Extensions
{
    /// <summary>
    /// 应用程序构建器扩展
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 映射爬虫控制器路由（使用 IApplicationBuilder）
        /// 注意：控制器已通过约定自动注册，此方法主要用于兼容性和未来扩展
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        /// <param name="configure">可选的自定义路由配置（注意：此配置不会影响已注册的路由约定）</param>
        /// <returns>应用程序构建器</returns>
        [System.Obsolete("路由已通过约定自动配置，此方法保留用于兼容性。如需自定义路由，请在 AddScraperController 中配置。")]
        public static IApplicationBuilder MapScraperController(this IApplicationBuilder app, System.Action<ScraperControllerOptions>? configure = null)
        {
            // 控制器已经通过约定自动注册路由，这里主要用于兼容性
            // 实际路由配置在 ScraperControllerRouteConvention 中完成
            // 如果提供了配置，记录警告但不应用（因为约定已经在启动时应用）
            if (configure != null)
            {
                var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
                var logger = loggerFactory?.CreateLogger("Aneiang.Pa.AspNetCore.Extensions.ApplicationBuilderExtensions");
                logger?.LogWarning("MapScraperController 的 configure 参数已弃用，请在 AddScraperController 中配置路由选项");
            }

            return app;
        }
    }
}

