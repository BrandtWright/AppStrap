namespace AppStrap.Notifications
{
    /// <summary>
    /// Defines a notification post-processor.
    /// </summary>
    public interface IPostProcessNotifications
    {
        /// <summary>
        /// Handles notification post-processing after a notification handler has been invoked.
        /// </summary>
        /// <param name="notification">The notification object.</param>
        void Handle(object notification);
    }

    /// <summary>
    /// Defines a notification post-processor.
    /// </summary>
    /// <typeparam name="TNotification">The type of notification.</typeparam>
    public interface IPostProcessNotifications<in TNotification> : IPostProcessNotifications
    {
        /// <summary>
        /// Handles notification post-processing after a notification handler has been invoked.
        /// </summary>
        /// <param name="notification">The notification object.</param>
        void Handle(TNotification notification);
    }
}