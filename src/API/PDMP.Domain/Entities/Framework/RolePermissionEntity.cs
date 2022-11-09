/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 0:49:42
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class RolePermissionEntity : BaseEntity
    {
        #region 成员(Member)
        /// <summary>
        /// 
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual RoleEntity Role { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long PermissionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual PermissionEntity Permission { get; set; }

        #endregion
    }
}

