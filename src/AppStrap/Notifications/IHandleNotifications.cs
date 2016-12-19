namespace AppStrap.Notifications
{
    /// <summary>
    /// Defines a handler for a notification.
    /// </summary>
    /// <typeparam name="TNotification">The type of notification to handle.</typeparam>
    public interface IHandleNotifications<in TNotification>
    {
        /// <summary>
        /// Handles a notification.
        /// </summary>
        /// <param name="notification">The notification object.</param>
        void Handle(TNotification notification);
    }
}