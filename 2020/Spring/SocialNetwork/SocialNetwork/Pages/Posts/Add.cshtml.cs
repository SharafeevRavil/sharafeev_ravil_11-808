using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialNetwork.Models;

namespace SocialNetwork.Pages.Posts
{
    public class Add : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public Add(IWebHostEnvironment env, IMapper mapper)
        {
            _env = env;
            _mapper = mapper;
        }

        [BindProperty] public AddedPost AddedPost { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string imageName = Guid.NewGuid() + new FileInfo(AddedPost.ImageFile.FileName).Extension;

            var imagesDirInfo = new DirectoryInfo(_env.WebRootPath + "/Images/");
            if (!imagesDirInfo.Exists)
            {
                imagesDirInfo.Create();
            }

            await using (var fileStream = new FileStream(imagesDirInfo.FullName + imageName, FileMode.Create))
            {
                await AddedPost.ImageFile.CopyToAsync(fileStream);
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

            Post post = _mapper.Map<Post>(AddedPost);
            post.Id = 1 + (posts.Count > 0 ? posts[^1].Id : -1);
            post.DateTime = DateTime.Now;
            post.ImageName = imageName;

            posts.Add(post);

            await using (var writer = new StreamWriter(postsFileInfo.FullName))
            {
                await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(posts);
            }

            return RedirectToPage("./Index");
        }
    }
}