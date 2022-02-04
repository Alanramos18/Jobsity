using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobsityChat.Data.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        ///     Get All.
        /// </summary>
        IQueryable<T> Get();

        /// <summary>
        ///     Gets the specific entity.
        /// </summary>
        /// <param name="entityId">Id of the entity</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int entityId, CancellationToken cancellationToken);

        /// <summary>
        ///     Adds the specific entity.
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        Task AddAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        ///     Adds the specifics entities.
        /// </summary>
        /// <param name="entities">Entities to be added.</param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        ///     Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        ///     Saves whatever entities have been added to the unit of work.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="cancellationToken">Cancellation Transaction Token</param>
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
