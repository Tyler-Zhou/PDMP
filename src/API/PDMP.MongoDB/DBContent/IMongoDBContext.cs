using MongoDB.Driver;

namespace PDMP.MongoDB.DBContent
{
    public interface IMongoDBContext : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        IMongoCollection<T> GetCollection<T>(string name);

        void AddCommand(Func<Task> func);
        Task<bool> SaveChanges();
    }
}
