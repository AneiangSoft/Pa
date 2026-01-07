using System;
using System.Text.Json;
using System.Threading.Tasks;
using Aneiang.Pa.AspNetCore.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Aneiang.Pa.AspNetCore.Caching
{
    /// <summary>
    /// Redis（分布式）缓存服务
    /// </summary>
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ScraperControllerOptions _options;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// ctor
        /// </summary>
        public RedisCacheService(IDistributedCache cache, IOptions<ScraperControllerOptions> options)
        {
            _cache = cache;
            _options = options.Value;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? duration = null)
        {
            var cached = await _cache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(cached))
            {
                try
                {
                    var obj = JsonSerializer.Deserialize<T>(cached, _jsonSerializerOptions);
                    if (obj != null)
                    {
                        return obj;
                    }
                }
                catch
                {
                    // 反序列化失败时，忽略缓存并重新生成
                }
            }

            var value = await factory();
            if (value == null)
            {
                // 兜底：避免写入 null，并保证返回非空（让调用方至少得到一次真实请求结果）
                value = await factory();
            }

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = duration ?? _options.CacheDuration
            };

            var json = JsonSerializer.Serialize(value, _jsonSerializerOptions);
            await _cache.SetStringAsync(key, json, options);

            return value;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Task RemoveAsync(string key)
        {
            return _cache.RemoveAsync(key);
        }
    }
}

