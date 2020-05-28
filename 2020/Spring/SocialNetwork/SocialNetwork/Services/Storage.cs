using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using SocialNetwork.Models;
using SocialNetwork.Models.Comments;

namespace SocialNetwork.Services
{
    public class Storage
    {
        private readonly IWebHostEnvironment _env;

        public Storage(IWebHostEnvironment env)
        {
            _env = env;
        }

        private void EnsureFileExists(string dirPath, string filePath)
        {
            var dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                fileInfo.Create().Close();
            }
        }

        #region Posts

        private string PostsDirectoryPath => _env.WebRootPath + "/Posts/";
        private string PostsFilePath => PostsDirectoryPath + "PostsData.csv";

        public List<Post> GetPosts()
        {
            EnsureFileExists(PostsDirectoryPath, PostsFilePath);
            using var reader = new StreamReader(PostsFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Post>().ToList();
        }

        private async Task WritePostsAsync(List<Post> posts)
        {
            await using var writer = new StreamWriter(PostsFilePath);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(posts);
        }

        public async Task AddPostAsync(Post post)
        {
            List<Post> posts = GetPosts();
            int index = posts.FindIndex(x => x.Id == post.Id);
            if (index == -1)
            {
                if (post.Id == -1)
                {
                    post.Id = (posts.LastOrDefault()?.Id ?? -1) + 1;
                }

                posts.Add(post);
            }
            else
            {
                posts.RemoveAt(index);
                posts.Insert(index, post);
            }

            await WritePostsAsync(posts);
        }

        public async Task DeletePostAsync(int id)
        {
            await WritePostsAsync(GetPosts().Where(x => x.Id != id).ToList());
        }

        public Post FindPost(int id)
        {
            return GetPosts().Find(x => x.Id == id);
        }

        #endregion

        #region Comments

        private string CommentsDirectoryPath => _env.WebRootPath + "/Comments/";
        private string CommentsFilePath => CommentsDirectoryPath + "CommentsData.csv";

        public List<Comment> GetComments()
        {
            EnsureFileExists(CommentsDirectoryPath, CommentsFilePath);
            using var reader = new StreamReader(CommentsFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Comment>().ToList();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            List<Comment> comments = GetComments();
            comment.Id = (comments.LastOrDefault()?.Id ?? -1) + 1;

            comments.Add(comment);

            await WriteCommentsAsync(comments);
        }

        private async Task WriteCommentsAsync(List<Comment> comments)
        {
            await using var writer = new StreamWriter(CommentsFilePath);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(comments);
        }

        #endregion
    }
}