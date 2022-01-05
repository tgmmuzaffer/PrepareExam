using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrepareExam.Models;
using PrepareExam.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrepareExam.Controllers
{
    public class wiredBlog
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepository _blogRepsitory;
        public HomeController(ILogger<HomeController> logger, IBlogRepository blogRepository)
        {
            _logger = logger;
            _blogRepsitory = blogRepository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            string baseurl = "https://www.wired.com";
            bool willblogget = false;

            try
            {
                var bloglist = await _blogRepsitory.GetListAsync();
                if (bloglist.Count != 0)
                {
                    var dateCheck = bloglist.Where(a => a.CreateDate.Day >= DateTime.Now.Day).ToList();
                    if (dateCheck.Count<5)
                    {
                        willblogget = true;
                        foreach (var item in bloglist)
                        {
                            await _blogRepsitory.Delete(item);
                        }
                    }
                }
                else
                {
                    willblogget = true;
                }

                if (willblogget)
                {
                    List<wiredBlog> wiredBlogs = new List<wiredBlog>();
                    HttpClient client = new HttpClient();
                    List<string> links = new List<string>();
                    using (var response = await client.GetAsync("https://www.wired.com/most-recent/"))
                    {
                        using (var content = response.Content)
                        {
                            var result = await content.ReadAsStringAsync();
                            var document = new HtmlDocument();
                            document.LoadHtml(result);
                            var nodes = document.DocumentNode.SelectNodes("//li[@class='archive-item-component']").Take(5);
                            foreach (var item in nodes)
                            {
                                var link = item.SelectSingleNode("a").Attributes["href"].Value;
                                links.Add(baseurl + link);
                            }
                        }
                    }


                    foreach (var itemlink in links)
                    {
                        using (var response_atag = await client.GetAsync(itemlink))
                        {
                            using (var content_atag = response_atag.Content)
                            {
                                var result_atag = await content_atag.ReadAsStringAsync();
                                var document_atag = new HtmlDocument();
                                document_atag.LoadHtml(result_atag);
                                var title = document_atag.DocumentNode.SelectNodes("//h1[@data-testid='ContentHeaderHed']").First().InnerText;
                                var body = document_atag.DocumentNode.SelectNodes("//div[@class='body__inner-container']").First().InnerText;
                                Blog wiredBlog = new Blog
                                {
                                    Title = title,
                                    Content = body,
                                    CreateDate = DateTime.Now
                                };
                                _ = await _blogRepsitory.Create(wiredBlog);
                            }
                        }
                    }
                }

                return View();
            }
            catch (Exception e)
            {
                new Exception("Blog içerikleri çekilirken hata oluştu.  " + e.Message);
            }
            return Redirect("Error/Error");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
