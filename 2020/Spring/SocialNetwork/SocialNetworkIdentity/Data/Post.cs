using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SocialNetworkIdentity.Data
{
    public class Post
    {
        public int Id { get; set; }
        
        public string Text { get; set; } 
        public DateTime DateTime { get; set; } 
        
        public List<Comment> Comments { get; set; }
        public IdentityUser Creator { get; set; }
    }
}