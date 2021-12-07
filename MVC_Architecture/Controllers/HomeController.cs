using Microsoft.AspNetCore.Mvc;
using MVC_Architecture.Models;
using MVC_Architecture.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Architecture.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        public HomeController(IPostService postService)
        {
            _postService = postService;
        }
        public IActionResult Index()
        {
            List<Post> postlar = _postService.GetAllPosts();

            return View("Index", postlar);
        }

        public IActionResult AddPost()
        {
            return View();
        }

        public IActionResult EditPost()
        {
            return View();
        }

        public IActionResult ViewPost()
        {
            return View();
        }
    }
}
