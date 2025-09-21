using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Interfaces.Repositories;
using EventManagementService.Domain.Interfaces.Specification;
using EventManagementService.Domain.Pagination;
using EventManagementService.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EventManagementService.Infrastructure.DataAccess.Repositories
{
    public abstract class BaseRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(e => e.Id!.Equals(id), cancellationToken);
        }

        public async Task<TEntity?> GetBySpecAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetListBySpecAsync(
            ISpecification<TEntity> spec, 
            PaginationOptions pagination,
            CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec)
                .ApplyPagination(pagination)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> СountBySpecAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(spec).CountAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return _dbSet.Where(spec.ToExpression());
        }
    }
}
