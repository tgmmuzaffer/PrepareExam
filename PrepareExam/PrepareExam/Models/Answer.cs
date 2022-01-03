using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Models
{
    public class Answer:IEntity
    {
        public int Id { get; set; }
        public string AnswerContent { get; set; }
        public bool IsCorrect { get; set; }
        [NotMapped]
        public string AnswerStatus { get; set; }
        public int QuestionId { get; set; }
    }
}
