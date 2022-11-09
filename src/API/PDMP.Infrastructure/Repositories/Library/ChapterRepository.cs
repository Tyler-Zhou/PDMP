/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 22:41:33
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using PDMP.Domain;

namespace PDMP.Infrastructure
{
    /// <summary>
    /// 章节存储库
    /// </summary>
    public class ChapterRepository : Repository<ChapterEntity>, IRepository<ChapterEntity>
    {
        #region 构造函数(Constructor)
        /// <summary>
        /// 章节存储库
        /// </summary>
        /// <param name="dbContext"></param>
        public ChapterRepository(LibraryDBContext dbContext) : base(dbContext)
        {
        }
        #endregion

    }
}

