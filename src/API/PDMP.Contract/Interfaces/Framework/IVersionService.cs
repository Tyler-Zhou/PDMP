/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-11-09 23:30:41
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
{
    /// <summary>
    /// 版本服务
    /// </summary>
    public interface IVersionService
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IPagedList<VersionInfo>> GetPagedListAsync(QueryParameter query);
        /// <summary>
        /// 根据主键Id获取单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<VersionInfo> GetSingleAsync(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<VersionInfo> AddAsync(VersionInfo dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<VersionInfo> UpdateAsync(VersionInfo dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);

    }
}

