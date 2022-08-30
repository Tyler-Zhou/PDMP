using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PDMP.Domain.Entities;
using PDMP.Domain.Interfaces;

namespace PDMP.Infrastructure.DBContent
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoDBContext : IMongoDBContext
    {
        /// <summary>
        /// MongoDB 设置对象
        /// </summary>
        MongoDBSetting _MongoDBSetting;
        /// <summary>
        /// 
        /// </summary>
        private IMongoDatabase Database { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IClientSessionHandle Session { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _Commands;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mongoDBSettings"></param>
        public MongoDBContext(IOptions<MongoDBSetting> mongoDBSettings)
        {
            _MongoDBSetting = mongoDBSettings.Value;
            _Commands = new List<Func<Task>>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveChanges()
        {
            ConfigureMongo();

            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _Commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _Commands.Count > 0;
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }
            MongoClient = new MongoClient(_MongoDBSetting.ConnectionString);
            Database = MongoClient.GetDatabase(_MongoDBSetting.DatabaseName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();
            return Database.GetCollection<T>(name);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        public void AddCommand(Func<Task> func)
        {
            _Commands.Add(func);
        }
    }
}
