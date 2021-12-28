using PrepareExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PrepareExam.Repository.IRepository
{
    public interface IBaseRepository<T> where T: class, IEntity
    {
        Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> filter = null);
        Task<T> Get(Expression<Func<T, bool>> filter = null);
        Task<T> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> IsExist(Expression<Func<T, bool>> filter = null);
    }
}
