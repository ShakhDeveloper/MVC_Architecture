using Microsoft.AspNetCore.Mvc;
using MVC_Architecture.Models;
using MVC_Architecture.Services;
using MVC_Architecture.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Architecture.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostService _postService;
        public BlogController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index()
        {
            List<Post> list = _postService.GetAllPosts();
            return View("Index", list);
        }
        [HttpGet]
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(BlogCreateViewModel viewModel)
        {
            Post newPost = null;
            if (ModelState.IsValid)
            {
                newPost = new Post()
                {
                    Title = viewModel.Title,
                    Body = viewModel.Body,
                    ImageFileName = _postService.SaveImage(viewModel.ImageFile)
                };
                newPost = _postService.AddPost(newPost);
            }

            return RedirectToAction("ViewPost", newPost);
        }

        [HttpGet]
        public IActionResult EditPost(Guid id)
        {
            Post post = _postService.GetById(id);
            BlogEditViewModel viewModel = new BlogEditViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                ImageFileName = post.ImageFileName
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult EditPost(BlogEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string imageSource = viewModel.ImageFileName;
                if (viewModel.ImageFile != null)
                {
                    imageSource = _postService.SaveImage(viewModel.ImageFile);
                }

                Post post = new Post()
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Body = viewModel.Body,
                    ImageFileName = imageSource
                };

                post = _postService.Update(post);

                return RedirectToAction("ViewPost", post);
            }
            return View();
        }

        public IActionResult ViewPost(Guid id)
        {
            Post post = _postService.GetById(id);
            return View("ViewPost", post);
        }

        public IActionResult DeletePost(Guid id)
        {
            _postService.DeletePost(id);

            return RedirectToAction("Index");
        }
    }
}
