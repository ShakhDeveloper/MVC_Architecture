using Microsoft.AspNetCore.Http;
using MVC_Architecture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Architecture.Services
{
   public interface IPostService
   {
        List<Post> GetAllPosts();
        Post GetById(Guid id);
        Post AddPost(Post newPost);
        Post Update(Post post);
        void DeletePost(Post post);
        string SaveImage(IFormFile file);
   }
}
