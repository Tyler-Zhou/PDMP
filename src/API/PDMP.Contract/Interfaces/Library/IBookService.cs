/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 23:35:56
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
{
    /// <summary>
    /// 书籍服务
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IPagedList<BookInfo>> GetPagedListAsync(QueryParameter query);
        /// <summary>
        /// 根据主键Id获取单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BookInfo> GetSingleAsync(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BookInfo> AddAsync(BookInfo dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BookInfo> UpdateAsync(BookInfo dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);
    }
}

