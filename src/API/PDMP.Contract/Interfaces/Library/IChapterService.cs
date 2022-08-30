using PDMP.Contract.DTOs;
using PDMP.Contract.Parameters;

namespace PDMP.Contract.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IChapterService : IBaseService<ChapterDto>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ServerResponse> GetAllAsync(ChapterParameter query);
    }
}
