/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 22:07:43
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Domain
{
    /// <summary>
    /// 书籍实体
    /// </summary>
    public class BookEntity : BaseEntity
    {
        #region 成员(Member)
        /// <summary>
        /// 标识键
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 作者Id
        /// </summary>
        public long AuthorId { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public virtual AuthorEntity Author { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 封面链接
        /// </summary>
        public string PosterLink { get; set; }
        /// <summary>
        /// 封面(二进制)
        /// </summary>
        public byte[] PosterContent { get; set; }

        /// <summary>
        /// 完本状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<ChapterEntity> Chapters { get; set; }
        #endregion
    }
}

