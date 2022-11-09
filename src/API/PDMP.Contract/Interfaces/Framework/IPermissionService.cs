/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 11:32:36
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IPagedList<PermissionInfo>> GetPagedListAsync(QueryParameter query);
        /// <summary>
        /// 根据主键Id获取单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PermissionInfo> GetSingleAsync(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PermissionInfo> AddAsync(PermissionInfo dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PermissionInfo> UpdateAsync(PermissionInfo dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);
    }
}

