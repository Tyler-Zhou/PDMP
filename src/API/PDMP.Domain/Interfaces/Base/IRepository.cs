using MongoDB.Driver;

namespace PDMP.Domain.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        #region 成员(Member)
        /// <summary>
        /// 集合名称
        /// </summary>
        string CollectionName { get; set; }
        #endregion

        #region Insert
        /// <summary>
        /// 异步新增实体
        /// </summary>
        /// <param name="entity">要新增的实体</param>
        /// <returns>A <see cref="Task"/>表示异步新增操作</returns>
        Task<bool> InsertAsync(TEntity entity);
        #endregion

        #region Delete
        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="id">主键ID</param>
        Task<bool> DeleteAsync(Guid id);
        #endregion

        #region Update
        /// <summary>
        /// 异步更新实体
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        Task<bool> UpdateAsync(TEntity entity);
        #endregion

        #region Single
        /// <summary>
        /// 异步获取第一个或默认实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>一个 <see cref="IPagedList{TEntity}"/>的元素</returns>
        Task<TEntity> GetFirstOrDefaultAsync(Guid id);
        /// <summary>
        /// 基于过滤、排序器异步获取第一个或默认实体
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序器</param>
        /// <returns>一个 <see cref="IPagedList{TEntity}"/> 包含满足 <paramref name="filter"/> 指定条件的元素</returns>
        Task<TEntity> GetFirstOrDefaultAsync(
            FilterDefinition<TEntity> filter = null,
            SortDefinition<TEntity> sort = null);
        #endregion

        #region PagedList
        /// <summary>
        /// 基于过滤、排序器异步获取 <see cref="IPagedList{TEntity}"/>
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序器</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>一个 <see cref="IPagedList{TEntity}"/> 包含满足 <paramref name="filter"/> 指定条件的元素</returns>
        Task<IPagedList<TEntity>> GetPagedListAsync(
            FilterDefinition<TEntity> filter = null,
            SortDefinition<TEntity> sort = null,
            int pageIndex = 0,
            int pageSize = 20);

        /// <summary>
        /// 基于投影、过滤、排序器异步获取 <see cref="IPagedList{TEntity}"/>
        /// </summary>
        /// <param name="projection">投影器</param>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序器</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>一个 <see cref="IPagedList{TEntity}"/> 包含满足 <paramref name="filter"/> 指定条件的元素</returns>
        Task<IPagedList<TEntity>> GetPagedListAsync(ProjectionDefinition<TEntity> projection,
            FilterDefinition<TEntity> filter = null,
            SortDefinition<TEntity> sort = null,
            int pageIndex = 0,
            int pageSize = 20);
        #endregion

        #region GetAll
        /// <summary>
        /// 异步获取所有实体
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();
        /// <summary>
        /// 基于过滤、排序器异步获取所有实体
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序器</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(
            FilterDefinition<TEntity> filter = null,
            SortDefinition<TEntity> sort = null);
        #endregion
    }
}
