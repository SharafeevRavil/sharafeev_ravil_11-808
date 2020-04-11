using System;

namespace SocialNetwork.Models
{
    public class Post
    {
        public Post()
        {
        }

        public Post(int id, string name, string text, string imageName, DateTime dateTime)
        {
            Id = id;
            Name = name;
            Text = text;
            ImageName = imageName;
            DateTime = dateTime;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public DateTime DateTime { get; set; }
    }
}