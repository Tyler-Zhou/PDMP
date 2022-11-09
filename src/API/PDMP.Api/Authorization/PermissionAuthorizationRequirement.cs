/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 11:45:25
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

using Microsoft.AspNetCore.Authorization;

namespace PDMP.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public PermissionAuthorizationRequirement(string name)
        {
            Name = name;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}
