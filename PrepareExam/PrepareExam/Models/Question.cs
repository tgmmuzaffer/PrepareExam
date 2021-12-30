using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Models
{
    public class Question:IEntity
    {
        public int Id { get; set; }
        public string QuestionContent { get; set; }
        public int AnswerId { get; set; }
        public List<Answer> Answers { get; set; }= new List<Answer>();
    }
}
