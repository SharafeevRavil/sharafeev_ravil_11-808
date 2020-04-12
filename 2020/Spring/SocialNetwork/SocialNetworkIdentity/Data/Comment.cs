using System;
using Microsoft.AspNetCore.Identity;

namespace SocialNetworkIdentity.Data
{
    public class Comment
    {
        public int Id { get; set; }
        
        public string Text { get; set; } 
        public DateTime DateTime { get; set; }
        
        public Post Post { get; set; }
        public IdentityUser Creator { get; set; }
    }
}