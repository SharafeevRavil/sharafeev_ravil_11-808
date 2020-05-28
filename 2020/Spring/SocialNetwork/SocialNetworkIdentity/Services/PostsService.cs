using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetworkIdentity.Data;
using SocialNetworkIdentity.ViewModels.Comments;
using SocialNetworkIdentity.ViewModels.Posts;

namespace SocialNetworkIdentity.Services
{
    public class PostsService
    {
        private readonly ApplicationDbContext _context;

        public PostsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PostCreateViewModel> GetByIdAsync(int id)
        {
            return new PostCreateViewModel{Text = (await _context.Posts.FindAsync(id)).Text};
        }

        public async Task<List<PostFullViewModel>> GetAsync()
        {
            return await _context.Posts
                .Include(x => x.Creator)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Creator)
                .Select(x => new PostFullViewModel(
                    x.Id,
                    x.Text,
                    x.DateTime,
                    x.Comments
                        .Select(y => new CommentFullViewModel(y.Id, y.Text, y.DateTime, y.Creator.Email))
                        .ToList(),
                    x.Creator.Email))
                .ToListAsync();
        }

        public async Task CreateAsync(string userId, PostCreateViewModel model)
        {
            var post = new Post
            {
                DateTime = DateTime.Now,
                Text = model.Text
            };

            var user = await _context.Users.FindAsync(userId);
            post.Creator = user;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CanEditAsync(string userId, int id)
        {
            var user = await _context.Users.FindAsync(userId);
            var post = await _context.Posts
                .Include(x => x.Creator)
                .FirstOrDefaultAsync(x => x.Id == id);
            return post != null && user != null && post.Creator.Id == user.Id;
        }

        public async Task EditAsync(int id, PostCreateViewModel model)
        {
            var post = await _context.Posts.FindAsync(id);
            post.Text = model.Text;
            _context.Update(post);
            await _context.SaveChangesAsync();
        }
    }
}