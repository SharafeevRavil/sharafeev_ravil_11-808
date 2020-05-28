using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkIdentity.Services;
using SocialNetworkIdentity.ViewModels.Posts;

namespace SocialNetworkIdentity.Controllers
{
    public class PostController : Controller
    {
        private readonly PostsService _postsService;

        public PostController(PostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _postsService.GetAsync());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _postsService.CreateAsync(userId, model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _postsService.CanEditAsync(userId, id))
            {
                ViewBag.Id = id;
                return View(await _postsService.GetByIdAsync(id));
            }

            return RedirectToAction("Index", "Post");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, PostCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (await _postsService.CanEditAsync(userId, id))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Id = id;
                    await _postsService.EditAsync(id, model); 
                    return RedirectToAction("Index", "Post");
                }
                return View(model);
            }

            return RedirectToAction("Index", "Post");
        }
    }
}