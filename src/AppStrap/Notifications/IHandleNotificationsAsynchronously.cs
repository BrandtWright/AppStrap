namespace AppStrap.Notifications
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an asynchronous handler for notifications.
    /// </summary>
    /// <typeparam name="TNotification">The type of notification to handle.</typeparam>
    public interface IHandleNotificationsAsynchronously<in TNotification>
        where TNotification : IAsyncNotification
    {
        /// <summary>
        /// Handles an asynchronous notification.
        /// </summary>
        /// <param name="notification">The notification object.</param>
        /// <returns>A task representing handling the notification.</returns>
        Task Handle(TNotification notification);
    }
}