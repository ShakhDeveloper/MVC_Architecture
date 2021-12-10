using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MVC_Architecture.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Architecture.Services
{
    public class PostService : IPostService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _dbContext;
        public PostService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public Post AddPost(Post newPost)
        {
            newPost.Id = Guid.NewGuid();
            newPost.CreatedTime = DateTime.Now;
            _dbContext.Add(newPost);
            _dbContext.SaveChanges();

            return newPost;
        }

        public void DeletePost(Guid id)
        {
            Post posts = _dbContext.posts.FirstOrDefault(p => p.Id == id);
            if (posts.ImageFileName is not null)
            {
                string uplodFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string filePath = Path.Combine(uplodFolder, posts.ImageFileName);
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
            }

            _dbContext.posts.Remove(posts);
            _dbContext.SaveChanges();
        }

        public List<Post> GetAllPosts()
        {
            return _dbContext.posts.ToList();
        }

        public Post GetById(Guid id)
        {
            return _dbContext.posts.FirstOrDefault(post => post.Id == id);
        }

        public string SaveImage(IFormFile newFile)
        {
            string uniqueName = string.Empty;
            if (newFile.FileName != null)
            {
                string uplodFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueName = Guid.NewGuid().ToString() + "_" + newFile.FileName;
                string filePath = Path.Combine(uplodFolder, uniqueName);
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                newFile.CopyTo(fileStream);
                fileStream.Close();
            }

            return uniqueName;
        }

        public Post Update(Post post)
        {
            _dbContext.posts.Update(post);
            _dbContext.SaveChanges();

            return post;
        }
    }
}
