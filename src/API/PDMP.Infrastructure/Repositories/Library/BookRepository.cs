using PDMP.Domain.Entities;
using PDMP.Domain.Interfaces;

namespace PDMP.Infrastructure.Repositories
{
    /// <summary>
    /// 书籍存储库
    /// </summary>
    public class BookRepository : Repository<BookInfo>, IBookRepository
    {
        /// <summary>
        /// 集合名称
        /// </summary>
        public override string CollectionName { get; set; } = "Books";

        /// <summary>
        /// 书籍存储库
        /// </summary>
        /// <param name="context">MongoDB上下文</param>
        public BookRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}
