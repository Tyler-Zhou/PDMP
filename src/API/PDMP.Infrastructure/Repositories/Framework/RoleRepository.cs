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
    /// 用户存储库
    /// </summary>
    public class RoleRepository : Repository<RoleEntity>, IRepository<RoleEntity>
    {
        #region 构造函数(Constructor)
        /// <summary>
        /// 用户存储库
        /// </summary>
        /// <param name="dbContext"></param>
        public RoleRepository(FrameworkDBContext dbContext) : base(dbContext)
        {
        } 
        #endregion
    }
}

