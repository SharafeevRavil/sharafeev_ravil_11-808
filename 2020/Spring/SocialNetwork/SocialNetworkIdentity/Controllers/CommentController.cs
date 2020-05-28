using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkIdentity.Services;
using SocialNetworkIdentity.ViewModels.Comments;

namespace SocialNetworkIdentity.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentsService _commentsService;

        public CommentController(CommentsService commentsService)
        {
            _commentsService = commentsService;
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Create(int postId)
        {
            ViewBag.PostId = postId;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(int postId, CommentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _commentsService.CreateAsync(userId, postId, model);
                return RedirectToAction("Index", "Post");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _commentsService.CanEditAsync(userId, id))
            {
                ViewBag.Id = id;
                return View(await _commentsService.GetByIdAsync(id));
            }

            return RedirectToAction("Index", "Post");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, CommentCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _commentsService.CanEditAsync(userId, id))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Id = id;
                    await _commentsService.EditAsync(id, model); 
                    return RedirectToAction("Index", "Post");
                }
                return View(model);
            }

            return RedirectToAction("Index", "Post");
        }
    }
}