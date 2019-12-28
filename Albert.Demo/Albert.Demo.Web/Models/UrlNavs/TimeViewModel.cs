using System;
using System.Collections.Generic;

namespace Albert.Demo.Web.Models.UrlNavs
{
    public class TimeViewModel
    {
        private int index = 0;
        public int Index { get { return index++; } }
        public IReadOnlyList<TimeDto> TimeDtos { get; set; }
        public TimeViewModel(IReadOnlyList<TimeDto> timeDtos)
        {
            TimeDtos = timeDtos;
        }
    }

    public class TimeDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
