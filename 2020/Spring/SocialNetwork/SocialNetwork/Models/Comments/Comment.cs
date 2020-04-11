using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace SocialNetwork.Models.Comments
{
    public class Comment
    {
        public int PostId { get; set; }
            
        public int Id { get; set; }
        
        [Display(Name = "Текст комментария")]  
        public string Text { get; set; }
    }
}