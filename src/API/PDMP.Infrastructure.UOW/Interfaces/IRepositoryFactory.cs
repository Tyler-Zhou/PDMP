/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-24 0:27:45
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace PDMP.Infrastructure
{
    /// <summary>
    /// Defines the interfaces for <see cref="IRepository{TEntity}"/> interfaces.
    /// </summary>
    public interface IRepositoryFactory
    {
        #region 方法(Method)
        /// <summary>
        /// Gets the specified repository for the <typeparamref name="TEntity"/>.
        /// </summary>
        /// <param name="hasCustomRepository"><c>True</c> if providing custom repositry</param>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>An instance of type inherited from <see cref="IRepository{TEntity}"/> interface.</returns>
        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class; 
        #endregion
    }
}

