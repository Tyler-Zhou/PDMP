/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 22:04:41
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using PDMP.Domain;

namespace PDMP.Infrastructure
{
    /// <summary>
    /// 系统版本存储库
    /// </summary>
    public class VersionRepository : Repository<VersionEntity>, IRepository<VersionEntity>
    {
        #region 构造函数(Constructor)
        /// <summary>
        /// 系统版本存储库
        /// </summary>
        /// <param name="dbContext"></param>
        public VersionRepository(FrameworkDBContext dbContext) : base(dbContext)
        {
        }
        #endregion
    }
}

