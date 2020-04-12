using System;
using Microsoft.AspNetCore.Identity;
using SocialNetworkIdentity.Data;

namespace SocialNetworkIdentity.ViewModels.Comments
{
    public class CommentFullViewModel
    {
        public CommentFullViewModel(int id, string text, DateTime dateTime, string creator)
        {
            Id = id;
            Text = text;
            DateTime = dateTime;
            Creator = creator;
        }

        public int Id { get; set; }

        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string Creator { get; set; }
    }
}