/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 11:45:25
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using PDMP.Contract;

namespace PDMP.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUserService _UserService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        public PermissionAuthorizationHandler(IUserService userService)
        {
            _UserService = userService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                // 如果是Admin角色就直接授权成功
                if (context.User.IsInRole("admin"))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //查询是否有权限
                    var userIdClaim = context.User.FindFirst(_ => _.Type == ClaimTypes.NameIdentifier);
                    if (userIdClaim != null)
                    {
                        var result=Task.Run(() => _UserService.CheckPermissionAsync(long.Parse(userIdClaim.Value), requirement.Name).Result).Result;
                        if (result)
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
