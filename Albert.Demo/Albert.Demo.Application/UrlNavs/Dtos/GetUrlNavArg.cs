using System;

namespace Albert.Demo.Application.UrlNavs.Dtos
{
    public class GetUrlNavArg
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Classify { get; set; }
    }
}
