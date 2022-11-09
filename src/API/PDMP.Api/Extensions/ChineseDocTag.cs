using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PDMP.Api.Extensions
{
    /// <summary>
    /// 中文文档标记
    /// </summary>
    public class ChineseDocTag : IDocumentFilter
    {
        /// <summary>
        /// 添加附加注释
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag { Name = "WebAuthenticator", Description = "验证控制器" },
                new OpenApiTag { Name = "Book", Description = "书籍控制器" },
                new OpenApiTag { Name = "Chapter", Description = "书籍章节控制器" },
            };
        }
    }
}
