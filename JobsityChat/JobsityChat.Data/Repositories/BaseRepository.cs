using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JobsityChat.Data;
using JobsityChat.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace RateService.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        protected readonly IChatDbContext _context;
        internal DbSet<T> _dbSet;

        protected BaseRepository(IChatDbContext chatContext)
        {
            _context = chatContext;
            this._dbSet = (_context as DbContext)?.Set<T>();
        }

        /// <inheritdoc />
        public IQueryable<T> Get()
        {
            return _context.Set<T>();
        }

        /// <inheritdoc />
        public virtual async Task<T> GetByIdAsync(int entityId, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(new object[] { entityId }, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        /// <inheritdoc />
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <inheritdoc />
        public virtual void Delete(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                await (_context as DbContext).SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
