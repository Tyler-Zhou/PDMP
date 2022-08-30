using PDMP.Client.Core.Models;
using PDMP.Client.Core.Services;
using PDMP.Library.Client.Interfaces;
using PDMP.Library.Client.Models;
using System.Threading.Tasks;

namespace PDMP.Library.Client.Services
{
    /// <summary>
    /// 书籍服务
    /// </summary>
    public class ChapterService : BaseService<ChapterModel>, IChapterService
    {
        /// <summary>
        /// 客户端服务
        /// </summary>
        private readonly ClientService _ClientService;
        /// <summary>
        /// 服务名称
        /// </summary>
        private const string _ServiceName = "Chapter";

        /// <summary>
        /// 书籍服务
        /// </summary>
        /// <param name="clientService">客户端服务</param>
        public ChapterService(ClientService clientService) : base(clientService, _ServiceName)
        {
            _ClientService = clientService;
        }
        /// <summary>
        /// 获取筛选后的分页书籍对象
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns></returns>
        public async Task<ReceiveResponse<PagedList<ChapterModel>>> GetAllFilterAsync(ChapterParameter parameter)
        {
            string filter = $"pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&search={parameter.Search}" +
                $"&bookKey={parameter.BookKey}";
            return await GetAllFilterAsync(filter);
        }

    }
}
