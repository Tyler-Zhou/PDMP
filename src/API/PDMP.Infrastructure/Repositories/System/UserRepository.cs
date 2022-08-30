using PDMP.Domain.Entities;
using PDMP.Domain.Interfaces;

namespace PDMP.Infrastructure.Repositories
{
    /// <summary>
    /// 用户存储库
    /// </summary>
    public class UserRepository : Repository<UserInfo>, IUserRepository
    {
        /// <summary>
        /// 集合名称
        /// </summary>
        public override string CollectionName { get; set; } = "Users";

        /// <summary>
        /// 用户存储库
        /// </summary>
        /// <param name="context">MongoDB上下文</param>
        public UserRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
