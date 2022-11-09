/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-22 1:22:00
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Domain
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class BaseEntity
    {
        #region 成员(Member)
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        #endregion
    }
}

