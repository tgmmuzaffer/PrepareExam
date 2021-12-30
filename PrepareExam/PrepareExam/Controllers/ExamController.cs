using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PrepareExam.Data;
using PrepareExam.Models;
using PrepareExam.Models.ViewModels;
using PrepareExam.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Controllers
{
    public class ExamController : BaseController
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IAnswerRepository _answerRepository;
        public ExamController(IBlogRepository blogRepository, IAnswerRepository answerRepository)
        {
            _blogRepository = blogRepository;
            _answerRepository=answerRepository;
        }
        public async Task<IActionResult> Prepare()
        {

            Exam exam = new Exam();

            var blogs = await _blogRepository.GetListAsync();
            List<SelectListItem> bloglist = new List<SelectListItem>();
            foreach (var item in blogs)
            {
                bloglist.Add(new SelectListItem() { Text = item.Title, Value = item.Id.ToString(), Selected = (item.Id == 1 ? true : false) });
            }
            ViewBag.Bloglist = bloglist;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetBlogContent(int id)
        {
            ExamPartsViewModel exam = new ExamPartsViewModel()
            {
                Blog = new Blog()
                {
                    Id = 1,
                    Title = "title",
                    Content = "Content"
                },
                Questions = new List<Question>()
                {
                    new Question()
                    {
                        Id = 1,
                        QuestionContent = "Questioncontent1",
                        Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                Id=1,
                                AnswerContent="Cevap11",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=2,
                                AnswerContent="Cevap21",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=3,
                                AnswerContent="Cevap31",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=4,
                                AnswerContent="Cevap41",
                                IsCorrect=true
                            },
                            
                        }
                    },
                    new Question()
                    {
                        Id = 2,
                        QuestionContent = "Questioncontent2",
                        Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                Id=1,
                                AnswerContent="Cevap12",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=2,
                                AnswerContent="Cevap22",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=3,
                                AnswerContent="Cevap32",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=4,
                                AnswerContent="Cevap42",
                                IsCorrect=true
                            },
                            
                        }
                    },
                    new Question()
                    {
                        Id = 3,
                        QuestionContent = "Questioncontent3",
                        Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                Id=1,
                                AnswerContent="Cevap13",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=2,
                                AnswerContent="Cevap23",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=3,
                                AnswerContent="Cevap33",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=4,
                                AnswerContent="Cevap43",
                                IsCorrect=true
                            },
                            
                        }
                    },
                    new Question()
                    {
                        Id = 4,
                        QuestionContent = "Questioncontent4",
                        Answers = new List<Answer>()
                        {
                            new Answer()
                            {
                                Id=1,
                                AnswerContent="Cevap14",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=2,
                                AnswerContent="Cevap24",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=3,
                                AnswerContent="Cevap34",
                                IsCorrect=false
                            },
                            new Answer()
                            {
                                Id=4,
                                AnswerContent="Cevap44",
                                IsCorrect=true
                            },
                            
                        }
                    },
                }
            };
            var t = JsonConvert.SerializeObject(exam);



            var res = await _blogRepository.Get(a => a.Id==id);
            var jsonres = JsonConvert.SerializeObject(res.Content);
            return Json(jsonres);
        }
    }
}
