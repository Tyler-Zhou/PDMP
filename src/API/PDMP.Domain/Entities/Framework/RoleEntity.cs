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
    public class RoleEntity : BaseEntity
    {
        #region 成员(Member)
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<RolePermissionEntity> RolePermissions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<UserRoleEntity> UserRoles { get; set; }
        #endregion
    }
}

