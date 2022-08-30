using PDMP.Contract.DTOs;

namespace PDMP.Contract.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService : IBaseService<UserDto>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ServerResponse> LoginAsync(string account, string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        Task<ServerResponse> Resgiter(UserDto userDto);
    }
}
