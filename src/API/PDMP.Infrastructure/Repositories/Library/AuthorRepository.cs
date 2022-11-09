/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 22:51:00
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using PDMP.Domain;

namespace PDMP.Infrastructure
{
    /// <summary>
    /// 作者存储库
    /// </summary>
    public class AuthorRepository : Repository<AuthorEntity>, IRepository<AuthorEntity>
    {
        #region 构造函数(Constructor)
        /// <summary>
        /// 作者存储库
        /// </summary>
        /// <param name="dbContext"></param>
        public AuthorRepository(LibraryDBContext dbContext) : base(dbContext)
        {
        }
        #endregion
    }
}

