using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrepareExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Data
{
    public class PrepareExamDbContext : IdentityDbContext
    {
        //IdentityDbContext
        public PrepareExamDbContext(DbContextOptions<PrepareExamDbContext> options) : base(options)
        {            
        }
        public virtual DbSet<Blog> Blogs { get; set; }

    }
}
