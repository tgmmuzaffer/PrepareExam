﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Models.ViewModels
{
    public class ExamPartsViewModel
    {
        public Blog Blog { get; set; }
        public List<Question> Questions { get; set; }= new List<Question>();
    }
    
}
