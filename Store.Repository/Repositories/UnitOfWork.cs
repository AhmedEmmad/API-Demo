using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _storeDbContext;
        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public async Task<int> CompleteAsync()
        => await _storeDbContext.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if(_repositories is null) 
                _repositories = new Hashtable();

            var entityKey = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(entityKey))
            {
                var repositoryType = typeof(GenericRepository<,>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity),typeof(TKey)), _storeDbContext);

                _repositories.Add(entityKey, repositoryInstance);
            }

            return (IGenericRepository<TEntity, TKey>)_repositories[entityKey];
        }
    }
}
