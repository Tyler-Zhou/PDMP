using MongoDB.Driver;
using PDMP.Domain.Interfaces;
using PDMP.Infrastructure.Extensions;
using ServiceStack;

namespace PDMP.Infrastructure.Repositories
{
    /// <summary>
    /// 表示默认的通用存储库实现了 <see cref="IRepository{TEntity}"/> 接口
    /// </summary>
    /// <typeparam name="TEntity">实体的类型</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 集合名称
        /// </summary>
        public virtual string CollectionName { get; set; } = "Defaults";

        /// <summary>
        /// 
        /// </summary>
        protected readonly IMongoDBContext _dbContext;
        /// <summary>
        /// 
        /// </summary>
        protected readonly IMongoCollection<TEntity> _dbSet;

        /// <summary>
        /// 初始化 <see cref="Repository{TEntity}"/> 类的新实例
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public Repository(IMongoDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.GetCollection<TEntity>(CollectionName);
        }

        #region Insert
        /// <summary>
        /// 异步新增实体
        /// </summary>
        /// <param name="entity">要新增的实体</param>
        /// <returns>A <see cref="Task"/>表示异步新增操作</returns>
        public virtual async Task<bool> InsertAsync(TEntity entity)
        {
            await _dbSet.InsertOneAsync(entity);
            return true;
        }
        #endregion

        #region Delete
        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="id">主键ID</param>
        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var deleteResult = await _dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("Id", id));
            return deleteResult.DeletedCount > 0;
        }
        #endregion

        #region Update
        /// <summary>
        /// 异步更新实体
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            var replaceOneResult = await _dbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("Id", entity.GetId()), entity);
            return replaceOneResult.ModifiedCount > 0;
        }
        #endregion

        #region Single
        /// <summary>
        /// 异步获取第一个或默认实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>一个 <see cref="IPagedList{TEntity}"/>的元素</returns>
        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Guid id)
        {
            var data = await _dbSet.FindAsync(Builders<TEntity>.Filter.Eq("Id", id));
            return data.SingleOrDefault();
        }
        /// <summary>
        /// 基于过滤、排序构建器异步获取第一个或默认实体
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序器</param>
        /// <returns>一个 <see cref="IPagedList{TEntity}"/> 包含满足 <paramref name="filter"/> 指定条件的元素</returns>
        public virtual async Task<TEntity> GetFirstOrDefaultAsync(
            FilterDefinition<TEntity> filter = null,
            SortDefinition<TEntity> sort = null)
        {
            return await _dbSet.Find(filter).Sort(sort).SingleOrDefaultAsync();
        }
        #endregion

        #region PagedList
        /// <summary>
        /// 基于判断、排序函数获取 <see cref="IPagedList{TEntity}"/>
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序器</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>一个 <see cref="IPagedList{TEntity}"/> 包含满足 <paramref name="filter"/> 指定条件的元素</returns>
        public virtual async Task<IPagedList<TEntity>> GetPagedListAsync(
            FilterDefinition<TEntity> filter = null,
            SortDefinition<TEntity> sort = null,
            int pageIndex = 0,
            int pageSize = 20)
        {
            return await _dbSet.Find(filter).Sort(sort).ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 基于投影、过滤、排序器异步获取 <see cref="IPagedList{TEntity}"/>
        /// </summary>
        /// <param name="projection">投影器</param>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序器</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>一个 <see cref="IPagedList{TEntity}"/> 包含满足 <paramref name="filter"/> 指定条件的元素</returns>
        public virtual async Task<IPagedList<TEntity>> GetPagedListAsync(ProjectionDefinition<TEntity> projection,
            FilterDefinition<TEntity> filter = null,
            SortDefinition<TEntity> sort = null,
            int pageIndex = 0,
            int pageSize = 20)
        {
            //TODO:.Project(projection)
            return await _dbSet.Find(filter).Sort(sort).ToPagedListAsync(pageIndex, pageSize);
        }
        #endregion

        #region GetAll
        /// <summary>
        /// 异步获取所有实体
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var all = await _dbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        /// <summary>
        /// 基于过滤、排序器异步获取所有实体
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序器</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            FilterDefinition<TEntity> filter = null,
            SortDefinition<TEntity> sort = null)
        {
            return await _dbSet.Find(filter).Sort(sort).ToListAsync();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
