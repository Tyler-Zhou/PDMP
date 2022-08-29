﻿using System.Threading.Tasks;

namespace PDMP.Client.Core
{
    /// <summary>
    /// Json服务
    /// </summary>
    public interface IJsonService
    {
        /// <summary>
        /// 保存Json文件
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <param name="obj">Json对象</param>
        Task<bool> SaveAsync(string configName,object obj);

        /// <summary>
        /// 获取Json
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <typeparam name="TJson">Json对象</typeparam>
        /// <returns></returns>
        Task<TJson> GetAsync<TJson>(string configName);
    }
}
