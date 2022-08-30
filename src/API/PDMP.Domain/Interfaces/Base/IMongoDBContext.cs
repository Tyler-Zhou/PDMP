using MongoDB.Driver;

namespace PDMP.Domain.Interfaces
{
    /// <summary>
    /// MongoDB上下文
    /// </summary>
    public interface IMongoDBContext : IDisposable
    {
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        IMongoCollection<T> GetCollection<T>(string name);

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="func"></param>
        void AddCommand(Func<Task> func);
        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChanges();
    }
}
