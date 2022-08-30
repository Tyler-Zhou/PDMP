using PDMP.Contract.DTOs;
using PDMP.Contract.Parameters;

namespace PDMP.Contract.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookService : IBaseService<BookDto>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ServerResponse> GetAllAsync(BookParameter query);
    }
}
