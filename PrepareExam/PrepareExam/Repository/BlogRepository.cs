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
            _dbContext = dbContext;
        }

        public async Task<Blog> Create(Blog entity)
        {
            _dbContext.Blogs.Add(entity);
             await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Blog entity)
        {
            _dbContext.Blogs.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Blog> Get(Expression<Func<Blog, bool>> filter = null)
        {
            try
            {
                var result = filter == null ? await _dbContext.Blogs.FirstOrDefaultAsync() : await _dbContext.Blogs.FirstOrDefaultAsync(filter);
                return result;            
            }
            catch (Exception e)
            {
                throw new Exception("Blog kaydedilirken bir hata oluştu. " + e.Message);
            }
        }

        public async Task<ICollection<Blog>> GetListAsync(Expression<Func<Blog, bool>> filter = null)
        {
            try
            {
                var result = filter == null ? await _dbContext.Blogs.ToListAsync() : await _dbContext.Blogs.Where(filter).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Blog kaydedilirken bir hata oluştu. " + e.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<Blog, bool>> filter)
        {
            try
            {
                var result = await _dbContext.Blogs.Where(filter).FirstOrDefaultAsync();
                if (result != null)
                    return true;
            }
            catch (Exception e)
            {
                throw new Exception("İşlem sırasında bir hata oluştu" + e.Message);
            }

            return false;
        }

        public async Task<bool> Update(Blog entity)
        {
            try
            {
                _dbContext.Blogs.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Soru güncellenemedi" + e.Message);

            }
        }
        //public async Task<Blog> Get(Expression<Func<Blog, bool>> filter = null)
        //{
        //    var result = filter == null ? await _dbContext.Blogs.FirstOrDefaultAsync() : await _dbContext.Blogs.FirstOrDefaultAsync(filter);
        //    return result;
        //}

        //public async Task<ICollection<Blog>> GetListAsync(Expression<Func<Blog, bool>> filter = null)
        //{
        //    var result = filter == null ? await _dbContext.Blogs.ToListAsync() : await _dbContext.Blogs.Where(filter).ToListAsync();
        //    return result;
        //}
    }
}
