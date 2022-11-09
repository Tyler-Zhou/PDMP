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
    /// 权限控制器
    /// </summary>
    [ApiController]
    [Route("PDMP/[controller]/[action]")]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IPermissionService _PermissionService;

        /// <summary>
        /// 权限控制器
        /// </summary>
        /// <param name="permissionService"></param>
        public PermissionController(IPermissionService permissionService)
        {
            _PermissionService = permissionService;
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
                return new ApiResponse(true, await _PermissionService.GetSingleAsync(id));
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
        public async Task<ApiResponse> GetPagedListAsync([FromQuery] QueryParameter param)
        {
            try
            {
                return new ApiResponse(true, await _PermissionService.GetPagedListAsync(param));
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
        public async Task<ApiResponse> Add([FromBody] PermissionInfo model)
        {
            try
            {
                return new ApiResponse(true, await _PermissionService.AddAsync(model));
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
        public async Task<ApiResponse> Update([FromBody] PermissionInfo model)
        {
            try
            {
                return new ApiResponse(true, await _PermissionService.UpdateAsync(model));
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
                return new ApiResponse(true, await _PermissionService.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
