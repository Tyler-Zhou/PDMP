using PDMP.Domain.Entities;
using PDMP.Domain.Interfaces;

namespace PDMP.Infrastructure.Repositories
{
    /// <summary>
    /// 章节存储库
    /// </summary>
    public class ChapterRepository : Repository<ChapterInfo>, IChapterRepository
    {
        /// <summary>
        /// 集合名称
        /// </summary>
        public override string CollectionName { get; set; } = "Chapters";

        /// <summary>
        /// 章节存储库
        /// </summary>
        /// <param name="context">MongoDB上下文</param>
        public ChapterRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
