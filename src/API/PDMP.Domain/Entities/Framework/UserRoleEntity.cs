/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 0:37:35
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRoleEntity : BaseEntity
    {
        #region 成员(Member)
        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual UserEntity User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual RoleEntity Role { get; set; }
        #endregion
    }
}

