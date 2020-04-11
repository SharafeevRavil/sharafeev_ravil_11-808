using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialNetwork.Models;

namespace SocialNetwork.Pages.Posts
{
    public class Index : PageModel
    {
        public List<Post> Posts { get; private set; }
        private readonly IWebHostEnvironment _env;

        public Index(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnGet()
        {
            var postsDirInfo = new DirectoryInfo(_env.WebRootPath + "/Posts/");
            if (!postsDirInfo.Exists)
            {
                postsDirInfo.Create();
            }

            var postsFileInfo = new FileInfo(postsDirInfo + "PostsData.csv");
            if (!postsFileInfo.Exists)
            {
                postsFileInfo.Create().Close();
            }

            using var reader = new StreamReader(postsFileInfo.FullName);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            Posts = csv.GetRecords<Post>().ToList();
        }

        public IActionResult OnPostDelete(int id)
        {
            var postsDirInfo = new DirectoryInfo(_env.WebRootPath + "/Posts/");
            if (!postsDirInfo.Exists)
            {
                postsDirInfo.Create();
            }

            var postsFileInfo = new FileInfo(postsDirInfo + "PostsData.csv");
            if (!postsFileInfo.Exists)
            {
                postsFileInfo.Create().Close();
            }

            List<Post> posts;
            using (var reader = new StreamReader(postsFileInfo.FullName))
            {
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                posts = csv.GetRecords<Post>().Where(x => x.Id != id).ToList();
            }

            
            using (var writer = new StreamWriter(postsFileInfo.FullName))
            {
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(posts);
            }

            return RedirectToPage();
        }
    }
}