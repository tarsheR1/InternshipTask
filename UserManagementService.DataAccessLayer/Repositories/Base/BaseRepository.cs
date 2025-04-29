using Microsoft.EntityFrameworkCore;
using UserManagementService.DataAccessLayer.Persistence;
using Shared.Interfaces;

namespace UserManagementService.DataAccessLayer.Repositories.Base
{
    public abstract class BaseRepository<TEntity, TKey> where TEntity : class 
    {
        protected readonly UserManagementDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(UserManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public virtual async Task<TEntity?> GetBySpecAsync(
            ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<List<TEntity>> GetAllBySpecAsync(
            ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<int> CountBySpecAsync(
            ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec).CountAsync(cancellationToken);
        }

        public virtual async Task<bool> AnyBySpecAsync(
            ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec)
                .AsNoTracking()
                .AnyAsync(cancellationToken);
        }

        protected virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            return query;
        }
    }
}
