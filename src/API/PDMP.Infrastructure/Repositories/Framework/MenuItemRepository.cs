/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-27 13:36:47
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using PDMP.Domain;

namespace PDMP.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuItemRepository : Repository<MenuItemEntity>, IRepository<MenuItemEntity>
    {
        #region 构造函数(Constructor)
        /// <summary>
        /// 权限存储库
        /// </summary>
        /// <param name="dbContext"></param>
        public MenuItemRepository(FrameworkDBContext dbContext) : base(dbContext)
        {
        }
        #endregion
    }
}

