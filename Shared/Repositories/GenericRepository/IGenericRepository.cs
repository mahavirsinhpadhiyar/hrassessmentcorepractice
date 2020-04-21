using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T: class
    {
        void Add(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        Task<T> GetSingle(Guid T);
        Task<int> GetTotalCount();
        void Update(T entiry);
    }
}
