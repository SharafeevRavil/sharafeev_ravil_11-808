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
using SocialNetwork.Models.Comments;
using SocialNetwork.Services;

namespace SocialNetwork.Pages.Posts
{
    public class Index : PageModel
    {
        public List<Post> Posts { get; private set; }
        public Dictionary<int, List<Comment>> Comments { get; private set; }
        private readonly IWebHostEnvironment _env;
        private readonly Storage _storage;

        public Index(IWebHostEnvironment env, Storage storage)
        {
            _env = env;
            _storage = storage;
        }

        public void OnGet()
        {
            Posts = _storage.GetPosts();
            Comments = _storage
                .GetComments()
                .GroupBy(x => x.PostId)
                .ToDictionary(x => x.Key, x => x.ToList());
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _storage.DeletePostAsync(id);

            return RedirectToPage();
        }
    }
}