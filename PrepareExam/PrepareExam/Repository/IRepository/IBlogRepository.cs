using PrepareExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PrepareExam.Repository.IRepository
{
    public interface IBlogRepository
    {
        Task<ICollection<Blog>> GetListAsync(Expression<Func<Blog, bool>> filter = null);
        Task<Blog> Get(Expression<Func<Blog, bool>> filter = null);
       
    }
}
