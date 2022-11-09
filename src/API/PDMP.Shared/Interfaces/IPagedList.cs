/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-21 18:57:35
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

using System.Collections.Generic;

namespace PDMP
{
    /// <summary>
    /// 为任何类型的分页列表提供接口
    /// </summary>
    /// <typeparam name="T">分页的类型</typeparam>
    public interface IPagedList<T>
    {
        #region 成员(Member)
        /// <summary>
        /// 获取索引起始值
        /// </summary>
        /// <value>索引起始值</value>
        int IndexFrom { get; }
        /// <summary>
        /// 获取页索引(当前)
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 获取页面大小
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 获取类型列表的总计数
        /// </summary>
        int TotalCount { get; }
        /// <summary>
        /// 获取页面总数
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// 获取当前页项
        /// </summary>
        IList<T> Items { get; }
        /// <summary>
        /// 获取前一页
        /// </summary>
        /// <value>前一页</value>
        bool HasPreviousPage { get; }

        /// <summary>
        /// 获取下一页
        /// </summary>
        /// <value>下一页</value>
        bool HasNextPage { get; } 
        #endregion
    }
}

