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

        public async Task<IActionResult> Exams()
        {
            List<ExamPartsViewModel> examParts = new List<ExamPartsViewModel>();

            var exams = await _examRepository.GetListAsync();

            foreach (var itemExam in exams)
            {
                ExamPartsViewModel examPart = new ExamPartsViewModel()
                {
                    Id=itemExam.Id,
                    Blog= await _blogRepository.Get(a => a.Id == itemExam.BlogId),
                    Questions= await _questionRepository.GetListAsync(a => a.ExamId == itemExam.Id),
                    CreateDate=itemExam.CreateDate
                };
                
                foreach (var itemExamQ in examPart.Questions)
                {
                    itemExamQ.Answers = await _answerRepository.GetListAsync(a => a.QuestionId == itemExamQ.Id);
                }
                examParts.Add(examPart);
            }

            var t = examParts;
            return View(examParts);
        }
        public async Task<IActionResult> DeleteExam(int Id)
        {
            return Json(1);
        }
    }
}
