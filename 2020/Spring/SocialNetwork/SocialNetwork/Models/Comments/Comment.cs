using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;
using SocialNetwork.Validation;

namespace SocialNetwork.Models.Comments
{
    public class Comment
    {
        public int PostId { get; set; }
            
        public int Id { get; set; }
        
        [Display(Name = "Текст комментария")]
        [NotEmpty(ErrorMessage = "Текст не должен быть пустым")]
        public string Text { get; set; }
    }
}