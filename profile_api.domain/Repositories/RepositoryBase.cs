using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using profile_api.domain.Repositories.Interfaces;

namespace profile_api.domain.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression) 
            => await _dbSet.Where(expression).ToListAsync();

        public async Task<IEnumerable<T>> FindAllAsync() 
            => await _dbSet.ToListAsync();

        public async Task<T?> FindByIdAsync(Guid id) 
            => await _dbSet.FindAsync(id).AsTask();

        public void Create(T entity) 
            => _dbSet.Add(entity);

        public void Update(T entity) 
            => _dbSet.Update(entity);

        public void Delete(T entity) 
            => _dbSet.Remove(entity);

        public async Task SaveChangesAsync() 
            => await _context.SaveChangesAsync();
    }
}
