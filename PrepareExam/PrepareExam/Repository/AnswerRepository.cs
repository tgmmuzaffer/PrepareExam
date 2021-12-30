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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly PrepareExamDbContext _context;
        public AnswerRepository(PrepareExamDbContext context)
        {
            _context = context;
        }
        public async Task<Answer> Create(Answer entity)
        {
            try
            {
                _context.Answers.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("Cevap kaydedilirken bir hata oluştu" + e.Message);
            }

        }

        public async Task<bool> Delete(Answer entity)
        {
            try
            {
                _context.Answers.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Cevap silinirken bir hata oluştu" + e.Message);
            }

        }

        public async Task<Answer> Get(Expression<Func<Answer, bool>> filter = null)
        {
            try
            {
                var result = filter == null ? await _context.Answers.FirstOrDefaultAsync() : await _context.Answers.FirstOrDefaultAsync(filter);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Cevap getirilirken bir hata oluştu" + e.Message);
            }

        }

        public async Task<ICollection<Answer>> GetListAsync(Expression<Func<Answer, bool>> filter = null)
        {
            try
            {
                var result = filter == null ? await _context.Answers.ToListAsync() : await _context.Answers.Where(filter).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Cevaplar getirilirken bir hata oluştu" + e.Message);
            }

        }

        public async Task<bool> IsExist(Expression<Func<Answer, bool>> filter)
        {
            try
            {
                var result = await _context.Answers.Where(filter).FirstOrDefaultAsync();
                if (result != null)
                    return true;
            }
            catch (Exception e)
            {
                throw new Exception("İşlem sırasında bir hata oluştu" + e.Message);
            }

            return false;
        }

        public async Task<bool> Update(Answer entity)
        {
            try
            {
                _context.Answers.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Cevap güncellenemedi" + e.Message);

            }

        }
    }
}
