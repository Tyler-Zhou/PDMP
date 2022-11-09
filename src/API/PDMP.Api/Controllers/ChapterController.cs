using Microsoft.AspNetCore.Mvc;
using PDMP.Contract;

namespace PDMP.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("PDMP/[controller]/[action]")]
    public class ChapterController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IChapterService _ChapterService;
        /// <summary>
        /// 章节章节控制器
        /// </summary>
        /// <param name="chapterService">章节服务</param>
        public ChapterController(IChapterService chapterService)
        {
            _ChapterService = chapterService;
        }
        /// <summary>
        /// 获取单本章节
        /// </summary>
        /// <param name="id">章节主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> Get(long id)
        {
            try
            {
                return new ApiResponse(true, await _ChapterService.GetSingleAsync(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 获取所有章节(分页)
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetPagedList([FromQuery] QueryParameter param)
        {
            try
            {
                return new ApiResponse(true, await _ChapterService.GetPagedListAsync(param));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 添加章节
        /// </summary>
        /// <param name="model">章节实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] ChapterInfo model)
        {
            try
            {
                return new ApiResponse(true, await _ChapterService.AddAsync(model));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 更新章节
        /// </summary>
        /// <param name="model">章节实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] ChapterInfo model)
        {
            try
            {
                return new ApiResponse(true, await _ChapterService.UpdateAsync(model));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="id">章节主键</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse> Delete(long id)
        {
            try
            {
                return new ApiResponse(true, await _ChapterService.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
