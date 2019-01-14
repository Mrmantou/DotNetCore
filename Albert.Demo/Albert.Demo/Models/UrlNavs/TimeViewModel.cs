using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albert.Demo.Models.UrlNavs
{
    public class TimeViewModel
    {
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
