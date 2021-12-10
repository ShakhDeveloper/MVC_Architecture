using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC_Architecture.View_Models
{
    public class BlogCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}