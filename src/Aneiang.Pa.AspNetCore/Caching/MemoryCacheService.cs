using System;
using System.Threading.Tasks;
using Aneiang.Pa.AspNetCore.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Aneiang.Pa.AspNetCore.Caching
{
    /// <summary>
    /// 内存缓存服务
    /// </summary>
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ScraperControllerOptions _options;

        /// <summary>
        /// ctor
        /// </summary>
        public MemoryCacheService(IMemoryCache memoryCache, IOptions<ScraperControllerOptions> options)
        {
            _memoryCache = memoryCache;
            _options = options.Value;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? duration = null)
        {
            var value = await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = duration ?? _options.CacheDuration;
                return await factory();
            });

            // IMemoryCache.GetOrCreateAsync 可能返回 null（取决于 factory 的实现），这里做一次兜底避免违反接口的非空约定
            if (value == null)
            {
                return await factory();
            }

            return value;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }
    }
}

