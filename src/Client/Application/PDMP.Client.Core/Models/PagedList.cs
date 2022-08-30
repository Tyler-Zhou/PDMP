using PDMP.Client.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PDMP.Client.Core.Models
{
    /// <summary>
    /// 表示 <see cref="IPagedList{T}"/> 接口的默认实现
    /// </summary>
    /// <typeparam name="T">需要分页的数据类型</typeparam>
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// 获得分页起始页
        /// </summary>
        /// <value>分页起始页</value>
        public int PageIndex { get; set; }
        /// <summary>
        /// 获得分页大小
        /// </summary>
        /// <value>分页大小</value>
        public int PageSize { get; set; }
        /// <summary>
        /// 获得总记录条数
        /// </summary>
        /// <value>总记录条数</value>
        public int TotalCount { get; set; }
        /// <summary>
        /// 获得总页数
        /// </summary>
        /// <value>总页数</value>
        public int TotalPages { get; set; }
        /// <summary>
        /// 获得索引起始位置
        /// </summary>
        /// <value>索引起始位置</value>
        public int IndexFrom { get; set; }

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <value>数据集</value>
        public IList<T> Items { get; set; }

        /// <summary>
        /// 获取前一页
        /// </summary>
        /// <value>前一页</value>
        public bool HasPreviousPage => PageIndex - IndexFrom > 0;

        /// <summary>
        /// 获取下一页
        /// </summary>
        /// <value>下一页</value>
        public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

        /// <summary>
        /// 初始化 <see cref="PagedList{T}" /> 类的新实例
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="indexFrom">索引起始位置</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }

            if (source is IQueryable<T> querable)
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = querable.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                Items = querable.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = source.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                Items = source.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();
            }
        }

        /// <summary>
        /// 初始化 <see cref="PagedList{T}" /> 类的新实例
        /// </summary>
        public PagedList() => Items = new T[0];
    }


    /// <summary>
    /// 提供 <see cref="IPagedList{T}"/> 和转换器的实现
    /// </summary>
    /// <typeparam name="TSource">源的类型</typeparam>
    /// <typeparam name="TResult">结果的类型</typeparam>
    public class PagedList<TSource, TResult> : IPagedList<TResult>
    {
        /// <summary>
        /// 获得分页起始页
        /// </summary>
        /// <value>分页起始页</value>
        public int PageIndex { get; }
        /// <summary>
        /// 获得分页大小
        /// </summary>
        /// <value>分页大小</value>
        public int PageSize { get; }
        /// <summary>
        /// 获得总记录条数
        /// </summary>
        /// <value>总记录条数</value>
        public int TotalCount { get; }
        /// <summary>
        /// 获得总页数
        /// </summary>
        /// <value>总页数</value>
        public int TotalPages { get; }
        /// <summary>
        /// 获得索引起始位置
        /// </summary>
        /// <value>索引起始位置</value>
        public int IndexFrom { get; }

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <value>数据集</value>
        public IList<TResult> Items { get; }

        /// <summary>
        /// 获取前一页
        /// </summary>
        /// <value>前一页</value>
        public bool HasPreviousPage => PageIndex - IndexFrom > 0;

        /// <summary>
        /// 获取下一页
        /// </summary>
        /// <value>下一页</value>
        public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

        /// <summary>
        /// 初始化 <see cref="PagedList{TSource, TResult}" /> 类的新实例
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="converter">转换器</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="indexFrom">索引起始位置</param>
        public PagedList(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageIndex: {pageIndex}, must indexFrom <= pageIndex");
            }

            if (source is IQueryable<TSource> querable)
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = querable.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                var items = querable.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToArray();

                Items = new List<TResult>(converter(items));
            }
            else
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = source.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                var items = source.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToArray();

                Items = new List<TResult>(converter(items));
            }
        }

        /// <summary>
        /// 初始化 <see cref="PagedList{TSource, TResult}" /> 类的新实例
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="converter">转换器</param>
        public PagedList(IPagedList<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            PageIndex = source.PageIndex;
            PageSize = source.PageSize;
            IndexFrom = source.IndexFrom;
            TotalCount = source.TotalCount;
            TotalPages = source.TotalPages;

            Items = new List<TResult>(converter(source.Items));
        }
    }

    /// <summary>
    /// 为 <see cref="IPagedList{T}"/> 接口提供一些帮助方法
    /// </summary>
    public static class PagedList
    {
        /// <summary>
        /// 创建一个空的 <see cref="IPagedList{T}"/>
        /// </summary>
        /// <typeparam name="T">分页类型</typeparam>
        /// <returns><see cref="IPagedList{T}"/> 的空实例</returns>
        public static IPagedList<T> Empty<T>() => new PagedList<T>();
        /// <summary>
        /// 从 <see cref="IPagedList{TSource}"/> 实例的源创建一个新的 <see cref="IPagedList{TResult}"/> 实例
        /// </summary>
        /// <typeparam name="TResult">结果集类型</typeparam>
        /// <typeparam name="TSource">数据源类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="converter">转换器</param>
        /// <returns><see cref="IPagedList{TResult}"/> 的一个实例</returns>
        public static IPagedList<TResult> From<TResult, TSource>(IPagedList<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter) => new PagedList<TSource, TResult>(source, converter);
    }
}
