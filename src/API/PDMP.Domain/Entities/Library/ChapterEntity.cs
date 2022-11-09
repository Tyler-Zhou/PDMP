/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 22:08:07
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Domain
{
    /// <summary>
    /// 章节实体
    /// </summary>
    public class ChapterEntity : BaseEntity
    {
        #region 成员(Member)
        /// <summary>
        /// 书籍标识键
        /// </summary>
        public string BookKey { get; set; }
        /// <summary>
        /// 书籍
        /// </summary>
        public virtual BookEntity Book { get; set; }
        /// <summary>
        /// 书籍Id
        /// </summary>
        public long BookId { get; set; }
        /// <summary>
        /// 标识键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 排序索引
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// 是否错误(章节)
        /// </summary>
        public bool IsError { get; set; } = false;
        #endregion
    }
}