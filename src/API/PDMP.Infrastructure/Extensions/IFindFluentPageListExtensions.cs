using MongoDB.Driver;
using PDMP.Domain.Entities;
using PDMP.Domain.Interfaces;

namespace PDMP.Infrastructure.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IFindFluentPageListExtensions
    {
        /// <summary>
        /// Converts the specified source to <see cref="IPagedList{T}"/> by the specified <paramref name="pageIndex"/> and <paramref name="pageSize"/>.
        /// 根据起始页和页大小转换成源
        /// </summary>
        /// <typeparam name="T">The type of the source.源的类型</typeparam>
        /// <param name="source">The source to paging.分页的源</param>
        /// <param name="pageIndex">The index of the page.起始页</param>
        /// <param name="pageSize">The size of the page.页大小</param>
        /// <param name="cancellationToken">
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        ///     在等待任务完成时观察
        /// </param>
        /// <param name="indexFrom">The start index value.值的起始索引</param>
        /// <returns>An instance of the inherited from <see cref="IPagedList{T}"/> interface.接口继承的实例</returns>
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IFindFluent<T, T> source, int pageIndex, int pageSize, int indexFrom = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            //如果索引比起始页大，则异常
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }
            //数据源大小
            var count = await source.CountDocumentsAsync(cancellationToken).ConfigureAwait(false);

            var items = await source.Skip((pageIndex - indexFrom) * pageSize)
                                    .Limit(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            var pagedList = new PagedList<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                IndexFrom = indexFrom,
                TotalCount = (int)count,
                Items = items,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            return pagedList;
        }
    }
}
