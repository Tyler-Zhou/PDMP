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
    /// 角色控制器
    /// </summary>
    [ApiController]
    [Route("PDMP/[controller]/[action]")]
    [Authorize]
    public class RoleController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRoleService _RoleService;

        /// <summary>
        /// 角色控制器
        /// </summary>
        /// <param name="roleService"></param>
        public RoleController(IRoleService roleService)
        {
            _RoleService = roleService;
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
                return new ApiResponse(true, await _RoleService.GetSingleAsync(id));
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
        public async Task<ApiResponse> GetPagedList([FromQuery] QueryParameter param)
        {
            try
            {
                return new ApiResponse(true, await _RoleService.GetPagedListAsync(param));
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
        public async Task<ApiResponse> Add([FromBody] RoleInfo model)
        {
            try
            {
                return new ApiResponse(true, await _RoleService.AddAsync(model));
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
        public async Task<ApiResponse> Update([FromBody] RoleInfo model)
        {
            try
            {
                return new ApiResponse(true, await _RoleService.UpdateAsync(model));
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
                return new ApiResponse(true, await _RoleService.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
