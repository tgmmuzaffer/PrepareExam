using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Models.ViewModels
{
    public class ExamPartsViewModel
    {
        public int Id { get; set; }
        public Blog Blog { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public DateTime CreateDate{ get; set; }
    }
    
}
