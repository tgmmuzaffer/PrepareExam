using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Models
{
    public class Exam : IEntity
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
