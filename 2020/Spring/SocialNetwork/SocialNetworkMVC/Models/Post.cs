using System;
using System.Collections.Generic;

namespace SocialNetworkMVC.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string ImageSource { get; set; }
        public DateTime DateTime { get; set; }

        public List<Comment> Comments { get; set; }
        public User Creator { get; set; }
    }
}