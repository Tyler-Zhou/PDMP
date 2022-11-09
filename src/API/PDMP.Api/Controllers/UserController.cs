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
    /// 用户控制器
    /// </summary>
    [ApiController]
    [Route("PDMP/[controller]/[action]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserService _UserService;

        /// <summary>
        /// 用户控制器
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _UserService = userService;
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
                return new ApiResponse(true, await _UserService.GetSingleAsync(id));
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
                return new ApiResponse(true, await _UserService.GetPagedListAsync(param));
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
        public async Task<ApiResponse> Add([FromBody] UserInfo model)
        {
            try
            {
                return new ApiResponse(true, await _UserService.AddAsync(model));
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
        public async Task<ApiResponse> Update([FromBody] UserInfo model)
        {
            try
            {
                return new ApiResponse(true, await _UserService.UpdateAsync(model));
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
                return new ApiResponse(true, await _UserService.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
