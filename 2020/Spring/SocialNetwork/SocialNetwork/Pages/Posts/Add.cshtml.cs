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
using SocialNetwork.Services;

namespace SocialNetwork.Pages.Posts
{
    public class Add : PageModel
    {
        private readonly ImagesService _imagesService;
        private readonly Storage _storage;

        public Add(Storage storage, ImagesService imagesService)
        {
            _storage = storage;
            _imagesService = imagesService;
        }

        [BindProperty] public AddedPost AddedPost { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _storage.AddPostAsync(await Post.GetPostFromAddedAsync(AddedPost, _imagesService));

            return RedirectToPage("./Index");
        }
    }
}