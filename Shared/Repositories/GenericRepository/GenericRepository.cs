using Microsoft.EntityFrameworkCore;
using Shared.UnitOfWork;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shared.Repositories.GenericRepository
{
    //article/tutorial
    //https://garywoodfine.com/generic-repository-pattern-net-core/
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        //private DbSet<T> table = null;
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //table = _unitOfWork._context.Set<T>();
        }
        public async void Add(T entity)
        {
            await _unitOfWork._context.AddAsync(entity);
            //await table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            //T existing = _unitOfWork._context.Set<T>().Find(entity);
            //if(existing != null)
            _unitOfWork._context.Remove(entity);
            //table.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _unitOfWork._context.Set<T>().AsQueryable<T>();
            //return await table.ToListAsync();
        }

        public async Task<int> GetTotalCount()
        {
            return await _unitOfWork._context.Set<T>().CountAsync();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _unitOfWork._context.Set<T>().Where(predicate).AsQueryable<T>();
        }

        public async Task<T> GetSingle(Guid T)
        {
            return await _unitOfWork._context.Set<T>().FindAsync(T);
            //return await table.FindAsync(T);
        }

        public void Update(T entity)
        {
            //table.Attach(entity);
            _unitOfWork._context.Set<T>().Attach(entity);
            _unitOfWork._context.Entry(entity).State = EntityState.Modified;
        }
    }
}
