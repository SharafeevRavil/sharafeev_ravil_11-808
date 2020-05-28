using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SocialNetworkIdentity.Data;
using SocialNetworkIdentity.ViewModels.Comments;

namespace SocialNetworkIdentity.ViewModels.Posts
{
    public class PostFullViewModel
    {
        public PostFullViewModel(int id, string text, DateTime dateTime, List<CommentFullViewModel> comments,
            string creator)
        {
            Id = id;
            Text = text;
            DateTime = dateTime;
            Comments = comments;
            Creator = creator;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        public List<CommentFullViewModel> Comments { get; set; }
        public string Creator { get; set; }
    }
}