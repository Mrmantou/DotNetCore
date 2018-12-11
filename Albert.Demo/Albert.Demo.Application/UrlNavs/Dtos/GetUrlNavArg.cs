using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Demo.Application.UrlNavs.Dtos
{
    public class GetUrlNavArg
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}
