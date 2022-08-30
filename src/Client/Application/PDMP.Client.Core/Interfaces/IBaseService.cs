using PDMP.Client.Core.Models;
using System;
using System.Threading.Tasks;

namespace PDMP.Client.Core.Interfaces
{
    /// <summary>
    /// 服务基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// 添加数据(TEntity)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ReceiveResponse<TEntity>> AddAsync(TEntity entity);
        /// <summary>
        /// 更新数据(TEntity)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ReceiveResponse<TEntity>> UpdateAsync(TEntity entity);
        /// <summary>
        /// 删除数据根据唯一键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ReceiveResponse> DeleteAsync(Guid id);
        /// <summary>
        /// 获取单个数据根据唯一键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ReceiveResponse<TEntity>> GetFirstOfDefaultAsync(Guid id);
        /// <summary>
        /// 获取分页数据(TEntity)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<ReceiveResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter);
        /// <summary>
        /// 获取分页数据(TEntity)
        /// </summary>
        /// <param name="filter">筛选字符串</param>
        /// <returns></returns>
        Task<ReceiveResponse<PagedList<TEntity>>> GetAllFilterAsync(string filter);
    }
}
