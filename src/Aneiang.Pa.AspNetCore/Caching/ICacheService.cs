using System;
using System.Threading.Tasks;

namespace Aneiang.Pa.AspNetCore.Caching
{
    /// <summary>
    /// 缓存服务接口
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// 从缓存中获取数据，如果不存在则执行委托并缓存结果
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="factory">当缓存不存在时，用于获取数据的委托</param>
        /// <param name="duration">缓存时长（可选，不传则使用默认时长）</param>
        /// <returns>缓存的数据</returns>
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? duration = null);

        /// <summary>
        /// 从缓存中移除数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        Task RemoveAsync(string key);
    }
}

