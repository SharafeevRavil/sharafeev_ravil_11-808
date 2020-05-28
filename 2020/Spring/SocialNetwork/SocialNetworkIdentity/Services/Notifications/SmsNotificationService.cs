using Microsoft.Extensions.Logging;

namespace SocialNetworkIdentity.Services.Notifications
{
    public class SmsNotificationService : INotificationService
    {
        private readonly ILogger<SmsNotificationService> _logger;

        public SmsNotificationService(ILogger<SmsNotificationService> logger)
        {
            _logger = logger;
        }

        public void NotifyAboutComment(string receiverId, int postId, string senderId, int commentId)
        {
            _logger.LogInformation($"SMS notification: " +
                                   $"User with id {senderId} has left comment with id {commentId} to " +
                                   $"post with id {postId} by user with id {receiverId}.");
        }
    }
}