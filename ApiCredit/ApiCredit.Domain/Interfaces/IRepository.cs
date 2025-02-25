using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiCredit.Domain.Interfaces
{
    public interface IRepository <T> where T : class
    {
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task Update(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetById(Guid id);
        Task<List<T>> GetByFilter(Expression<Func<T, bool>> filter);
        Task Save();
    }
}
