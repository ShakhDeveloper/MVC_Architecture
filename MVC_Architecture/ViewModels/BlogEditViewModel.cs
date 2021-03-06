using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MVC_Architecture.View_Models
{
    public class BlogEditViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string ImageFileName { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}