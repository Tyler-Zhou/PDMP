using PDMP.Contract.DTOs;
using PDMP.Contract.Parameters;

namespace PDMP.Contract.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T>
    {
        /// <summary>
        /// 服务基接口
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ServerResponse> GetAllAsync(QueryParameter query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServerResponse> GetSingleAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ServerResponse> AddAsync(T dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ServerResponse> UpdateAsync(T dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServerResponse> DeleteAsync(Guid id);
    }
}
