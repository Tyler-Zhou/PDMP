/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 11:45:25
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDMP.Contract;

namespace PDMP.Api.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    [ApiController]
    [Route("PDMP/[controller]/[action]")]
    [Authorize]
    public class MenuItemController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMenuItemService _MenuItemService;

        /// <summary>
        /// 菜单控制器
        /// </summary>
        /// <param name="permissionService"></param>
        public MenuItemController(IMenuItemService permissionService)
        {
            _MenuItemService = permissionService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> Get(long id)
        {
            try
            {
                return new ApiResponse(true, await _MenuItemService.GetSingleAsync(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetAll(long userId)
        {
            try
            {
                return new ApiResponse(true, await _MenuItemService.GetAllAsync(userId));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetPageList([FromQuery] QueryParameter param)
        {
            try
            {
                return new ApiResponse(true, await _MenuItemService.GetPagedListAsync(param));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] MenuItemInfo model)
        {
            try
            {
                return new ApiResponse(true, await _MenuItemService.AddAsync(model));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] MenuItemInfo model)
        {
            try
            {
                return new ApiResponse(true, await _MenuItemService.UpdateAsync(model));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse> Delete(long id)
        {
            try
            {
                return new ApiResponse(true, await _MenuItemService.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
