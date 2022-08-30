using PDMP.Client.Core.Interfaces;
using PDMP.Client.Core.Models;
using PDMP.Library.Client.Models;
using System.Threading.Tasks;

namespace PDMP.Library.Client.Interfaces
{
    /// <summary>
    /// 书籍服务
    /// </summary>
    public interface IBookService : IBaseService<BookModel>
    {
        /// <summary>
        /// 获取筛选后的分页书籍对象
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns></returns>
        Task<ReceiveResponse<PagedList<BookModel>>> GetAllFilterAsync(BookParameter parameter);
    }
}
