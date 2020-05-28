using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Models
{
    public class EditedPost
    {
        [Display(Name = "Название")]  
        public string Name { get; set; }
       
        [Display(Name = "Текст")]  
        public string Text { get; set; }
       
        [Display(Name = "Изображение")]  
        public IFormFile ImageFile { get; set; }
    }
}