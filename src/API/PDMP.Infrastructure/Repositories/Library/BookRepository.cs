/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 22:41:20
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using PDMP.Domain;

namespace PDMP.Infrastructure
{
    /// <summary>
    /// 书籍存储库
    /// </summary>
    public class BookRepository : Repository<BookEntity>, IRepository<BookEntity>
    {
        #region 构造函数(Constructor)
        /// <summary>
        /// 书籍存储库
        /// </summary>
        /// <param name="dbContext"></param>
        public BookRepository(LibraryDBContext dbContext) : base(dbContext)
        {
        }
        #endregion

    }
}

