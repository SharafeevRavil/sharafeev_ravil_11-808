using Microsoft.Extensions.Logging;

namespace SocialNetworkIdentity.Services.Notifications
{
    public class EmailNotificationService : INotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;

        public EmailNotificationService(ILogger<EmailNotificationService> logger)
        {
            _logger = logger;
        }

        public void NotifyAboutComment(string receiverId, int postId, string senderId, int commentId)
        {
            _logger.LogInformation($"Email notification: " +
                                   $"User with id {senderId} has left comment with id {commentId} to " +
                                   $"post with id {postId} by user with id {receiverId}.");
        }
    }
}