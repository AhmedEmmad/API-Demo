using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _storeDbContext;

        public GenericRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public async Task AddAsync(TEntity entity)
        => await _storeDbContext.Set<TEntity>().AddAsync(entity);

        public async Task DeleteAsync(TEntity entity)
        => _storeDbContext.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        => await _storeDbContext.Set<TEntity>().ToListAsync();


        public async Task<TEntity> GetByIdAsync(TKey? id)
        => await _storeDbContext.Set<TEntity>().FindAsync(id);

        public async Task UpdateAsync(TEntity entity)
        => _storeDbContext.Set<TEntity>().Update(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> spcs)
            => await SpecificationEvaluator<TEntity, TKey>.GetQuery(_storeDbContext.Set<TEntity>(), spcs).ToListAsync();
        public async Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> spcs)
            => await SpecificationEvaluator<TEntity, TKey>.GetQuery(_storeDbContext.Set<TEntity>(), spcs).FirstOrDefaultAsync();

    }
}
