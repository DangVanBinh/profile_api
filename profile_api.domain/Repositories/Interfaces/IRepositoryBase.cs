using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace profile_api.domain.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T?> FindByIdAsync(Guid id);
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
