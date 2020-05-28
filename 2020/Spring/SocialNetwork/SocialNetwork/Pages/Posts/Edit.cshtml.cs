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
using SocialNetwork.Services;

namespace SocialNetwork.Pages.Posts
{
    public class Edit : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly Storage _storage;
        private readonly ImagesService _imagesService;

        public Edit(IWebHostEnvironment env, Storage storage, ImagesService imagesService)
        {
            _env = env;
            _storage = storage;
            _imagesService = imagesService;
        }

        [BindProperty] public Post Post { get; set; }
        [BindProperty] public EditedPost EditedPost { get; set; }

        public IActionResult OnGet(int id)
        {
            Post = _storage.FindPost(id);
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

            Post post = _storage.FindPost(Post.Id);
            if (post == null)
            {
                return NotFound();
            }
            await post.ApplyChanges(EditedPost, _imagesService);

            await _storage.AddPostAsync(post);

            return RedirectToPage("./Index");
        }
    }
}