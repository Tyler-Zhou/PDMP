/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-27 13:43:03
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

using System.Collections.ObjectModel;

namespace PDMP.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMenuItemService
    {
        /// <summary>
        /// 获取数据(权限)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ObservableCollection<MenuItemInfo>> GetAllAsync(long userId);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IPagedList<MenuItemInfo>> GetPagedListAsync(QueryParameter query);
        /// <summary>
        /// 根据主键Id获取单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MenuItemInfo> GetSingleAsync(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<MenuItemInfo> AddAsync(MenuItemInfo dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<MenuItemInfo> UpdateAsync(MenuItemInfo dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long id);
    }
}

