﻿using System;

namespace Albert.Demo.Domain.Friends
{
    /// <summary>
    /// 朋友实体
    /// </summary>
    public class Friend
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 关系
        /// </summary>
        public RelationType RelationType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
