/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-27 13:32:12
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuItemEntity : BaseEntity
    {
        #region 成员(Member)
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CommandParameter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual MenuItemEntity Parent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<MenuItemEntity> Children { get; set; }
        #endregion
    }
}

