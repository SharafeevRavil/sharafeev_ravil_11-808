using System;
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
    public class Edit : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public Edit(IWebHostEnvironment env)
        {
            _env = env;
        }

        [BindProperty] public Post Post { get; set; }
        [BindProperty] public EditedPost EditedPost { get; set; }

        public IActionResult OnGet(int id)
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
            Post = csv.GetRecords<Post>().FirstOrDefault(x => x.Id == id);
            if (Post == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

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
                posts = csv.GetRecords<Post>().ToList();
            }

            //FIXME:: get post from file
            Post post = posts.FirstOrDefault(x => x.Id == Post.Id);
            if (post == null)
            {
                return NotFound();
            }

            if (EditedPost.ImageFile != null)
            {
                string imageName = Guid.NewGuid() + new FileInfo(EditedPost.ImageFile.FileName).Extension;

                var imagesDirInfo = new DirectoryInfo(_env.WebRootPath + "/Images/");
                if (!imagesDirInfo.Exists)
                {
                    imagesDirInfo.Create();
                }

                await using var fileStream = new FileStream(imagesDirInfo.FullName + imageName, FileMode.Create);
                await EditedPost.ImageFile.CopyToAsync(fileStream);

                post.ImageName = imageName;
            }

            if (!String.IsNullOrEmpty(EditedPost.Name))
            {
                post.Name = EditedPost.Name;
            }

            if (!String.IsNullOrEmpty(EditedPost.Text))
            {
                post.Text = EditedPost.Text;
            }

            await using (var writer = new StreamWriter(postsFileInfo.FullName))
            {
                await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(posts);
            }

            return RedirectToPage("./Index");
        }
    }
}