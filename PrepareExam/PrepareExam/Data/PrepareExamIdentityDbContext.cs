using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrepareExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Data
{
    public class PrepareExamIdentityDbContext : IdentityDbContext
    {
        public PrepareExamIdentityDbContext(DbContextOptions<PrepareExamIdentityDbContext> options) : base(options)
        {            
        }

    }
}
