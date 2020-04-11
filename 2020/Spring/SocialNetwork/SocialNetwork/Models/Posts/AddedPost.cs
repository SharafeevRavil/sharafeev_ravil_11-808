using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Models
{
    public class AddedPost
    {
        [Required(ErrorMessage = "Добавьте название")]  
        [Display(Name = "Название")]  
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Добавьте текст")]  
        [Display(Name = "Текст")]  
        public string Text { get; set; }
        
        [Required(ErrorMessage = "Добавьте изображение")]  
        [Display(Name = "Изображение")]  
        public IFormFile ImageFile { get; set; }
    }
}