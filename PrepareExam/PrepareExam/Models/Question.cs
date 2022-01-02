using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Models
{
    public class Question:IEntity
    {
        public int Id { get; set; }
        public string QuestionContent { get; set; }
        public int ExamId { get; set; }
        public ICollection<Answer> Answers { get; set; }= new List<Answer>();
    }
}
