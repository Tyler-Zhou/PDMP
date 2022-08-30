using PDMP.Client.Core.Interfaces;
using PDMP.Client.Core.Models;
using PDMP.Library.Client.Models;
using System.Threading.Tasks;

namespace PDMP.Library.Client.Interfaces
{
    /// <summary>
    /// 章节服务
    /// </summary>
    public interface IChapterService : IBaseService<ChapterModel>
    {
        /// <summary>
        /// 获取筛选后的分页章节对象
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns></returns>
        Task<ReceiveResponse<PagedList<ChapterModel>>> GetAllFilterAsync(ChapterParameter parameter);
    }
}
