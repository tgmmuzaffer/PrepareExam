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
    public class ExamRepository : IExamRepository
    {
        private readonly PrepareExamDbContext _context;
        public ExamRepository(PrepareExamDbContext context)
        {
            _context = context;
        }
        public async Task<Exam> Create(Exam entity)
        {
            try
            {
                _context.Exams.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception e)
            {
                throw new Exception("Sınav kaydedilirken bir hata oluştu. " + e.Message);
            }
        }

        public async Task<bool> Delete(Exam entity)
        {
            try
            {
                _context.Exams.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Sınav silinirken bir hata oluştu" + e.Message);
            }
        }

        public async Task<Exam> Get(Expression<Func<Exam, bool>> filter = null)
        {
            try
            {
                var result = filter == null ? await _context.Exams.FirstOrDefaultAsync() : await _context.Exams.FirstOrDefaultAsync(filter);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Sınav getirilirken bir hata oluştu" + e.Message);
            }
        }

        public async Task<ICollection<Exam>> GetListAsync(Expression<Func<Exam, bool>> filter = null)
        {
            try
            {
                var result = filter == null ? await _context.Exams.ToListAsync() : await _context.Exams.Where(filter).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Sınavlar getirilirken bir hata oluştu" + e.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<Exam, bool>> filter)
        {
            try
            {
                var result = await _context.Exams.Where(filter).FirstOrDefaultAsync();
                if (result != null)
                    return true;
            }
            catch (Exception e)
            {
                throw new Exception("İşlem sırasında bir hata oluştu" + e.Message);
            }

            return false;
        }

        public async Task<bool> Update(Exam entity)
        {
            try
            {
                _context.Exams.Update(entity);
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
