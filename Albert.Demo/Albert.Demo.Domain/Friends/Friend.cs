using Albert.Domain.Entities;

namespace Albert.Demo.Domain.Friends
{
    /// <summary>
    /// 朋友实体
    /// </summary>
    public class Friend : Entity
    {
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
