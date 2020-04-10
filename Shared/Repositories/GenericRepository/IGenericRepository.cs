using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T: class
    {
        void Add(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> Get();
        Task<IEnumerable<T>> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        void Update(T entiry);
    }
}
