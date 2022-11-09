/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 0:32:42
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using PDMP.Domain;

namespace PDMP.Infrastructure
{
    /// <summary>
    /// 角色权限存储库
    /// </summary>
    public class RolePermissionRepository : Repository<RolePermissionEntity>, IRepository<RolePermissionEntity>
    {
        #region 构造函数(Constructor)
        /// <summary>
        /// 角色权限存储库
        /// </summary>
        /// <param name="dbContext"></param>
        public RolePermissionRepository(FrameworkDBContext dbContext) : base(dbContext)
        {
        } 
        #endregion
    }
}

