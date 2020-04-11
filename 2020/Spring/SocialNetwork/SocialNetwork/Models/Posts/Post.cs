using System;
using System.Threading.Tasks;
using SocialNetwork.Services;

namespace SocialNetwork.Models
{
    public class Post
    {
        public static async Task<Post> GetPostFromAddedAsync(AddedPost addedPost, ImagesService imagesService)
        {
            return new Post
            {
                Id = -1,
                Name = addedPost.Name,
                Text = addedPost.Text,
                DateTime = DateTime.Now,
                ImageName = await imagesService.LoadImageAsync(addedPost.ImageFile)
            };
        }

        public async Task ApplyChanges(EditedPost editedPost, ImagesService imagesService)
        {
            if (editedPost.ImageFile != null)
            {
                ImageName = await imagesService.LoadImageAsync(editedPost.ImageFile);
            }

            if (!String.IsNullOrEmpty(editedPost.Name))
            {
                Name = editedPost.Name;
            }

            if (!String.IsNullOrEmpty(editedPost.Text))
            {
                Text = editedPost.Text;
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public DateTime DateTime { get; set; }
    }
}