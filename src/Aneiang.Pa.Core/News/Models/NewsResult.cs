using System.Collections.Generic;

namespace Aneiang.Pa.Core.News.Models
{
    /// <summary>
    /// 新闻返回模型
    /// </summary>
    public class NewsResult
    {
        /// <summary>
        /// 初始化新闻结果
        /// </summary>
        /// <param name="isSuccessd">是否成功</param>
        /// <param name="errorMessage">错误消息</param>
        public NewsResult(bool isSuccessd = true, string? errorMessage = null)
        {
            IsSuccessd = isSuccessd;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccessd { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 数据列表
        /// </summary>
        public List<NewsItem> Data { get; set; } = new List<NewsItem>();

        /// <summary>
        /// 创建成功结果
        /// </summary>
        /// <returns>成功的结果对象</returns>
        public static NewsResult Success() => new NewsResult(true);

        /// <summary>
        /// 创建失败结果
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <returns>失败的结果对象</returns>
        public static NewsResult Failure(string errorMessage) => new NewsResult(false, errorMessage);
    }
}
