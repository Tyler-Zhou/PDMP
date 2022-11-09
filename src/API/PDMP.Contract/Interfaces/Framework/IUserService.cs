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
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserInfo> AuthorizationAsync(string userName, string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        Task<bool> CheckPermissionAsync(long userId, string permissionName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<UserInfo> GetSingleAsync(string userName);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IPagedList<UserInfo>> GetPagedListAsync(QueryParameter query);
        /// <summary>
        /// 根据主键Id获取单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserInfo> GetSingleAsync(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<UserInfo> AddAsync(UserInfo dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<UserInfo> UpdateAsync(UserInfo dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);

    }
}

