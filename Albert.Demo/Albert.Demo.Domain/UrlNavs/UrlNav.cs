using Albert.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Demo.Domain.UrlNavs
{
    /// <summary>
    /// 网址导航
    /// </summary>
    public class UrlNav : Entity<Guid>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Classify { get; set; }
        /// <summary>
        /// 网页地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 内容描述
        /// </summary>
        public string Description { get; set; }
    }
}
