using PDMP.Domain.Entities;
using PDMP.Domain.Interfaces;

namespace PDMP.Infrastructure.Repositories
{
    /// <summary>
    /// 版本存储库
    /// </summary>
    public class VersionRepository : Repository<VersionInfo>, IVersionRepository
    {
        /// <summary>
        /// 集合名称
        /// </summary>
        public override string CollectionName { get; set; } = "Versions";

        /// <summary>
        /// 版本存储库
        /// </summary>
        /// <param name="context">MongoDB上下文</param>
        public VersionRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
