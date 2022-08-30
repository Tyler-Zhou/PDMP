using Microsoft.AspNetCore.Mvc;
using PDMP.Contract.DTOs;
using PDMP.Contract.Interfaces;

namespace PDMP.Api.Controllers
{
    /// <summary>
    /// 验证控制器
    /// </summary>
    [ApiController]
    [Route("PDMP/[controller]/[action]")]
    public class WebAuthenticatorController : ControllerBase
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUserService _UserService;
        /// <summary>
        /// 验证控制器
        /// </summary>
        /// <param name="userService">登录服务</param>
        public WebAuthenticatorController(IUserService userService)
        {
            _UserService = userService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServerResponse> Login([FromBody] UserDto param) =>
           await _UserService.LoginAsync(param.Account, param.Password);
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServerResponse> Resgiter([FromBody] UserDto param) =>
            await _UserService.Resgiter(param);
    }
}
