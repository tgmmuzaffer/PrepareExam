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
        private readonly IQuestionRepository _questionRepository;
        private readonly IExamRepository _examRepository;
        public ExamController(IBlogRepository blogRepository, IAnswerRepository answerRepository, IExamRepository examRepository, IQuestionRepository questionRepository)
        {
            _blogRepository = blogRepository;
            _answerRepository = answerRepository;
            _examRepository = examRepository;
            _questionRepository = questionRepository; ;
        }
        [Route("Sinav-Hazirla")]
        public async Task<IActionResult> Prepare()
        {
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
            var res = await _blogRepository.Get(a => a.Id == id);
            var jsonres = JsonConvert.SerializeObject(res.Content);
            return Json(jsonres);
        }


        [HttpPost]
        public async Task<JsonResult> SaveExam(ExamPartsViewModel exam)
        {
            Exam _exam = new Exam { BlogId = exam.Blog.Id, CreateDate = DateTime.Now };
            var examRes = await _examRepository.Create(_exam);
            foreach (var itemQ in exam.Questions)
            {
                Question question = new Question { ExamId = examRes.Id, QuestionContent = itemQ.QuestionContent };
                var questionRes = await _questionRepository.Create(question);
                foreach (var itemA in itemQ.Answers)
                {
                    Answer answer = new Answer { AnswerContent = itemA.AnswerContent, IsCorrect = itemA.IsCorrect, QuestionId = questionRes.Id };
                    _ = await _answerRepository.Create(answer);
                }
            }

            return Json(1);
        }
        [Route("Sinavlar")]
        public async Task<IActionResult> Exams()
        {
            List<ExamPartsViewModel> examParts = new List<ExamPartsViewModel>();

            var exams = await _examRepository.GetListAsync();

            foreach (var itemExam in exams)
            {
                ExamPartsViewModel examPart = new ExamPartsViewModel()
                {
                    Id = itemExam.Id,
                    Blog = await _blogRepository.Get(a => a.Id == itemExam.BlogId),
                    Questions = await _questionRepository.GetListAsync(a => a.ExamId == itemExam.Id),
                    CreateDate = itemExam.CreateDate
                };

                foreach (var itemExamQ in examPart.Questions)
                {
                    itemExamQ.Answers = await _answerRepository.GetListAsync(a => a.QuestionId == itemExamQ.Id);
                }
                examParts.Add(examPart);
            }
            return View(examParts);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteExam(int Id)
        {
            Exam exam = await _examRepository.Get(a => a.Id == Id);
            bool isQuestionDelete = false;
            bool isAnswerDelete = false;

            ICollection<Question> questions = await _questionRepository.GetListAsync(b => b.ExamId == exam.Id);
            foreach (var itemQ in questions)
            {
                ICollection<Answer> answers = await _answerRepository.GetListAsync(c => c.QuestionId == itemQ.Id);
                foreach (var itemA in answers)
                {
                    isAnswerDelete = await _answerRepository.Delete(itemA);
                }

                isQuestionDelete = await _questionRepository.Delete(itemQ);
            }
            bool isExamDelete = await _examRepository.Delete(exam);

            if (isExamDelete && isQuestionDelete && isAnswerDelete)
            {
                return Json(1);
            }

            return Json(0);
        }

        public async Task<IActionResult> DoExam(int Id)
        {
            Exam exam = await _examRepository.Get(a => a.Id == Id);
            ICollection<Question> questions = await _questionRepository.GetListAsync(a => a.ExamId == exam.Id);
            foreach (var itemQ in questions)
            {
                ICollection<Answer> answers = await _answerRepository.GetListAsync(b => b.QuestionId == itemQ.Id);
            }
            ExamPartsViewModel examPart = new ExamPartsViewModel()
            {
                Id = exam.Id,
                Blog = await _blogRepository.Get(a => a.Id == exam.BlogId),
                Questions = questions
            };
            return View(examPart);
        }
        [Route("Sinav-Sonucu")]
        public async Task<IActionResult> ExamResult(string examid, string id)
        {

            var examId = JsonConvert.DeserializeObject<string>(examid);
            var userAnwersIds = JsonConvert.DeserializeObject<List<string>>(id);
            Exam exam = await _examRepository.Get(a => a.Id == Convert.ToInt32(examId));
            ICollection<Question> questions = await _questionRepository.GetListAsync(a => a.ExamId == exam.Id);
            foreach (var itemQ in questions)
            {
                ICollection<Answer> answers = await _answerRepository.GetListAsync(b => b.QuestionId == itemQ.Id);
                    foreach (var itemA in answers)
                    {
                        if (itemA.IsCorrect && userAnwersIds.Any(a => a.Contains(itemA.Id.ToString())))
                        {
                            itemA.AnswerStatus = "true";
                        }
                        else if (itemA.IsCorrect && !userAnwersIds.Any(a => a.Contains(itemA.Id.ToString())))
                        {
                            itemA.AnswerStatus = "false";
                        }
                        else
                        {
                            itemA.AnswerStatus = "";
                        }
                    }
            }
            ExamPartsViewModel examPart = new ExamPartsViewModel()
            {
                Id = exam.Id,
                Blog = await _blogRepository.Get(a => a.Id == exam.BlogId),
                Questions = questions
            };
            return View(examPart);
        }
    }
}
