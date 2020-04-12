using System;

namespace SocialNetworkMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        
        public Post Post { get; set; }
        public User Creator { get; set; }
    }
}