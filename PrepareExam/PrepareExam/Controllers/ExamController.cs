using Microsoft.AspNetCore.Mvc;
using PrepareExam.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrepareExam.Controllers
{
    public class ExamController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        public ExamController(IBlogRepository blogRepository)
        {
            _blogRepository= blogRepository;
        }
        public async Task<IActionResult> Prepare()
        {
            var t= await _blogRepository.GetListAsync();
            return View();
        }
    }
}
