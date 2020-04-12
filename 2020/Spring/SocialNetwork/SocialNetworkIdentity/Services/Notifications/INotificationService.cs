namespace SocialNetworkIdentity.Services.Notifications
{
    public interface INotificationService
    {
        void NotifyAboutComment(string receiverId, int postId, string senderId, int commentId);
    }
}