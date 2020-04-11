using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialNetwork.Models.Comments;
using SocialNetwork.Services;

namespace SocialNetwork.Pages.Posts.Comments
{
    public class Add : PageModel
    {
        private readonly Storage _storage;

        public Add(Storage storage)
        {
            _storage = storage;
        }

        public void OnGet(int id)
        {
            PostId = id;
        }

        [BindProperty] public int PostId { get; set; }
        [BindProperty] public Comment Comment { get; set; }

        public async Task<IActionResult> OnPostAsync(int postId)
        {
            Comment.PostId = postId;
            await _storage.AddCommentAsync(Comment);
            return RedirectToPage("./../Index");
        }
    }
}