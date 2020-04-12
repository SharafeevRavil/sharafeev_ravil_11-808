using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetworkIdentity.Data;
using SocialNetworkIdentity.ViewModels.Comments;

namespace SocialNetworkIdentity.Services
{
    public class CommentsService
    {
        private readonly ApplicationDbContext _context;

        public CommentsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommentCreateViewModel> GetByIdAsync(int id)
        {
            return new CommentCreateViewModel {Text = (await _context.Comments.FindAsync(id)).Text};
        }

        public async Task CreateAsync(string userId, int postId, CommentCreateViewModel model)
        {
            var comment = new Comment
            {
                DateTime = DateTime.Now,
                Text = model.Text,
                Post = await _context.Posts.FindAsync(postId)
            };

            var user = await _context.Users.FindAsync(userId);
            comment.Creator = user;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CanEditAsync(string userId, int id)
        {
            var user = await _context.Users.FindAsync(userId);
            var comment = await _context.Comments
                .Include(x => x.Creator)
                .FirstOrDefaultAsync(x => x.Id == id);
            return comment != null && user != null && comment.Creator.Id == user.Id &&
                   (DateTime.Now - comment.DateTime).Minutes <= 15;
        }

        public async Task EditAsync(int id, CommentCreateViewModel model)
        {
            var comment = await _context.Comments.FindAsync(id);
            comment.Text = model.Text;
            _context.Update(comment);
            await _context.SaveChangesAsync();
        }
    }
}