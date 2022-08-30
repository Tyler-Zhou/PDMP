using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PDMP.Contract.DTOs;
using PDMP.Contract.Interfaces;

namespace PDMP.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("PDMP/[controller]/[action]")]
    public class DownloadController : ControllerBase
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger<DownloadController> _Logger;

        private const int BufferSize = 80 * 1024;

        private const string MimeType = "application/octet-stream";
        private IFileService _FileService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private IHttpContextAccessor _contextAccessor;
        /// <summary>
        /// 
        /// </summary>
        private HttpContext _context 
        { 
            get 
            { 
                return _contextAccessor.HttpContext; 
            } 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="fileService"></param>
        /// <param name="contextAccessor"></param>
        public DownloadController(ILogger<DownloadController> logger, IFileService fileService, IHttpContextAccessor contextAccessor)
        {
            _Logger = logger;
            _FileService = fileService;
            _contextAccessor = contextAccessor;
        }


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetFile(string fileName)
        {
            if (!_FileService.Exists(fileName))
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            //获取下载文件长度
            var fileLength = _FileService.GetLength(fileName);

            //初始化下载文件信息
            var fileInfo = GetFileInfoFromRequest(_context.Request, fileLength);

            //获取剩余部分文件流
            var stream = new PartialContentFileStream(_FileService.Open(fileName),
                                                 fileInfo.From, fileInfo.To);
            //设置响应 请求头
            SetResponseHeaders(_context.Response, fileInfo, fileLength, fileName);

            return new FileStreamResult(stream, new MediaTypeHeaderValue(MimeType));
        }

        /// <summary>
        /// 根据请求信息赋予封装的文件信息类
        /// </summary>
        /// <param name="request"></param>
        /// <param name="entityLength"></param>
        /// <returns></returns>
        private FileDto GetFileInfoFromRequest(HttpRequest request, long entityLength)
        {
            var fileInfo = new FileDto
            {
                From = 0,
                To = entityLength - 1,
                IsPartial = false,
                Length = entityLength
            };

            var requestHeaders = request.GetTypedHeaders();

            if (requestHeaders.Range != null && requestHeaders.Range.Ranges.Count > 0)
            {
                var range = requestHeaders.Range.Ranges.FirstOrDefault();
                if (range.From.HasValue && range.From < 0 || range.To.HasValue && range.To > entityLength - 1)
                {
                    return null;
                }

                var start = range.From;
                var end = range.To;

                if (start.HasValue)
                {
                    if (start.Value >= entityLength)
                    {
                        return null;
                    }
                    if (!end.HasValue || end.Value >= entityLength)
                    {
                        end = entityLength - 1;
                    }
                }
                else
                {
                    if (end.Value == 0)
                    {
                        return null;
                    }

                    var bytes = Math.Min(end.Value, entityLength);
                    start = entityLength - bytes;
                    end = start + bytes - 1;
                }

                fileInfo.IsPartial = true;
                fileInfo.Length = end.Value - start.Value + 1;
            }
            return fileInfo;
        }

        /// <summary>
        /// 设置响应头信息
        /// </summary>
        /// <param name="response"></param>
        /// <param name="fileInfo"></param>
        /// <param name="fileLength"></param>
        /// <param name="fileName"></param>
        private void SetResponseHeaders(HttpResponse response, FileDto fileInfo,long fileLength, string fileName)
        {
            response.Headers[HeaderNames.AcceptRanges] = "bytes";
            response.StatusCode = fileInfo.IsPartial ? StatusCodes.Status206PartialContent
                                      : StatusCodes.Status200OK;

            var contentDisposition = new ContentDispositionHeaderValue("attachment");
            contentDisposition.SetHttpFileName(fileName);
            response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
            response.Headers[HeaderNames.ContentType] = MimeType;
            response.Headers[HeaderNames.ContentLength] = fileInfo.Length.ToString();
            if (fileInfo.IsPartial)
            {
                response.Headers[HeaderNames.ContentRange] = new ContentRangeHeaderValue(fileInfo.From, fileInfo.To, fileLength).ToString();
            }
        }
    }
}
