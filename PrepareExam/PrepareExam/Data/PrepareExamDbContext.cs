using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrepareExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Data
{
    public class PrepareExamDbContext : DbContext
    {
        public PrepareExamDbContext(DbContextOptions<PrepareExamDbContext> options) : base(options)
        {            
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }

    }
}
