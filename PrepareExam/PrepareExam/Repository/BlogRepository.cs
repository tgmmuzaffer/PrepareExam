using Microsoft.EntityFrameworkCore;
using PrepareExam.Data;
using PrepareExam.Models;
using PrepareExam.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PrepareExam.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly PrepareExamDbContext _dbContext;
        public BlogRepository(PrepareExamDbContext dbContext)
        {
            _dbContext=dbContext;
        }
        public async Task<Blog> Get(Expression<Func<Blog, bool>> filter = null)
        {
            var result = filter == null ? await _dbContext.Blogs.FirstOrDefaultAsync() : await _dbContext.Blogs.FirstOrDefaultAsync(filter);
            return result;
        }

        public async Task<ICollection<Blog>> GetListAsync(Expression<Func<Blog, bool>> filter = null)
        {
            var result = filter == null ? await _dbContext.Blogs.ToListAsync() : await _dbContext.Blogs.Where(filter).ToListAsync();
            return result;
        }
    }
}
