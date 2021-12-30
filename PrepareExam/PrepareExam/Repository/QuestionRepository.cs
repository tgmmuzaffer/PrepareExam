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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly PrepareExamDbContext _context;
        public QuestionRepository(PrepareExamDbContext context)
        {
            _context = context;
        }
        public async Task<Question> Create(Question entity)
        {
            try
            {
                _context.Questions.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("Soru kaydedilirken bir hata oluştu" + e.Message);
            }
        }

        public async Task<bool> Delete(Question entity)
        {
            try
            {
                _context.Questions.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Soru silinirken bir hata oluştu" + e.Message);
            }
        }

        public async Task<Question> Get(Expression<Func<Question, bool>> filter = null)
        {
            try
            {
                var result = filter == null ? await _context.Questions.FirstOrDefaultAsync() : await _context.Questions.FirstOrDefaultAsync(filter);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Soru getirilirken bir hata oluştu" + e.Message);
            }
        }

        public async Task<ICollection<Question>> GetListAsync(Expression<Func<Question, bool>> filter = null)
        {
            try
            {
                var result = filter == null ? await _context.Questions.ToListAsync() : await _context.Questions.Where(filter).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Sorular getirilirken bir hata oluştu" + e.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<Question, bool>> filter)
        {
            try
            {
                var result = await _context.Questions.Where(filter).FirstOrDefaultAsync();
                if (result != null)
                    return true;
            }
            catch (Exception e)
            {
                throw new Exception("İşlem sırasında bir hata oluştu" + e.Message);
            }

            return false;
        }

        public async Task<bool> Update(Question entity)
        {
            try
            {
                _context.Questions.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Soru güncellenemedi" + e.Message);

            }
        }
    }
}
