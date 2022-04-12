using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using BlogSystem.Models;

namespace CheShi
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task<TEntity> FindAsync(object key);

        void Delete(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
