using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aneiang.Pa.Dynamic.Attributes
{
    /// <summary>
    /// 数据值特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HtmlValueAttribute : Attribute
    {
        /// <summary>
        /// HTML标签
        /// </summary>
        public string HtmlTag { get; set; }

        /// <summary>
        /// HTML属性
        /// </summary>
        public string? HtmlAttribute { get; set; }


        /// <summary>
        /// HTML Id
        /// </summary>
        public string? HtmlId { get; set; }

        /// <summary>
        /// HTML Class
        /// </summary>
        public string? HtmlClass { get; set; }

        /// <summary>
        /// 存在多个，需要指定索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 数据值特性
        /// </summary>
        /// <param name="htmlTag">HTML标签</param>
        /// <param name="attribute">抓取的属性，例如：href、src</param>
        /// <param name="htmlId">HTML Id</param>
        /// <param name="htmlClass">HTML Class</param>
        /// <param name="index">存在多个，需要指定索引</param>
        public HtmlValueAttribute(string htmlTag, string? attribute = null, string? htmlId = null, string? htmlClass = null, int index = 0)
        {
            HtmlTag = htmlTag;
            HtmlAttribute = attribute;
            HtmlId = htmlId;
            HtmlClass = htmlClass;
            Index = index;
        }
    }
}
