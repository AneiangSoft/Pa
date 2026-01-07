using System;
using System.Threading.Tasks;

namespace Aneiang.Pa.AspNetCore.Caching
{
    /// <summary>
    /// 不启用缓存的实现
    /// </summary>
    public class NoCacheService : ICacheService
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? duration = null)
        {
            return await factory();
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public Task RemoveAsync(string key)
        {
            return Task.CompletedTask;
        }
    }
}

