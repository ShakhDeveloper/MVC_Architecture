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

        private readonly ApplicationDbContext _dbContext;

        public PostService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Post AddPost(Post newPost)
        {
            _dbContext.Add(newPost);
            _dbContext.SaveChanges();

            return newPost;
        }

        public void DeletePost(Post post)
        {
            _dbContext.posts.Remove(post);
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

        public string SaveImage(IFormFile file)
        {
            string uniqueName = String.Empty;
            if (file.FileName != " ")
            {
                string uploadFile = Path.Combine("Images");
                uniqueName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadFile, uniqueName);
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
                fileStream.Close();
            }
            return uniqueName;
        }

        public Post Update(Post post)
        {
            _dbContext.posts.Update(post);
            _dbContext.SaveChanges();

            return (post);
        }
    }
}
