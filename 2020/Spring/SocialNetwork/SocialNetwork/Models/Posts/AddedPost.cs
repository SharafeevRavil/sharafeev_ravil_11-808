using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Validation;

namespace SocialNetwork.Models
{
    public class AddedPost
    {
        [Display(Name = "Название")]
        [RegularExpr("(^(Пост))(.+)", ErrorMessage = "Название поста должно начинаться с \"Пост\"")]
        public string Name { get; set; }

        [Display(Name = "Текст")]
        [NotEmpty(ErrorMessage = "Добавьте текст")]
        public string Text { get; set; }

        [Display(Name = "Изображение")]
        [Required(ErrorMessage = "Добавьте изображение")]
        public IFormFile ImageFile { get; set; }
    }
}