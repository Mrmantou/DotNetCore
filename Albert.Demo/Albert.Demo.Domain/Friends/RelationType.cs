using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Albert.Demo.Domain.Friends
{
    public enum RelationType
    {
        /// <summary>
        /// 同学
        /// </summary>
        [Description("同学")]
        Classmate = 1,
        /// <summary>
        /// 同事
        /// </summary>
        [Description("同事")]
        Workmate = 2,
        /// <summary>
        /// 朋友
        /// </summary>
        [Description("朋友")]
        Friend = 3,
        /// <summary>
        /// 老师
        /// </summary>
        [Description("老师")]
        Teacher =4,
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Others = 99
    }
}
