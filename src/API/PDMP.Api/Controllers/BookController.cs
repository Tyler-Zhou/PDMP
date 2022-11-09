using Microsoft.AspNetCore.Mvc;
using PDMP.Contract;

namespace PDMP.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("PDMP/[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _BookService;
        /// <summary>
        /// 书籍控制器
        /// </summary>
        /// <param name="bookService">书籍服务</param>
        public BookController(IBookService bookService)
        {
            _BookService = bookService;
        }

        /// <summary>
        /// 获取单本书籍
        /// </summary>
        /// <param name="id">书籍主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> Get(long id)
        {
            try
            {
                return new ApiResponse(true, await _BookService.GetSingleAsync(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 获取所有书籍(分页)
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetPagedList([FromQuery] QueryParameter param)
        {
            try
            {
                return new ApiResponse(true, await _BookService.GetPagedListAsync(param));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="model">书籍实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] BookInfo model)
        {
            try
            {
                return new ApiResponse(true, await _BookService.AddAsync(model));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 更新书籍
        /// </summary>
        /// <param name="model">书籍实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] BookInfo model)
        {
            try
            {
                return new ApiResponse(true, await _BookService.UpdateAsync(model));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="id">书籍主键</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse> Delete(long id)
        {
            try
            {
                return new ApiResponse(true, await _BookService.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
