namespace AppStrap.Notifications
{
    /// <summary>
    /// Defines a notification pre-processor.
    /// </summary>
    public interface IPreProcessNotifications
    {
        /// <summary>
        /// Handles notification pre-processing before a notification handler is invoked.
        /// </summary>
        /// <param name="notification">The type of notification.</param>
        void Handle(object notification);
    }

    /// <summary>
    /// Defines a notification pre-processor.
    /// </summary>
    /// <typeparam name="TNotification">The type of notification.</typeparam>
    public interface IPreProcessNotifications<in TNotification> : IPreProcessNotifications
    {
        /// <summary>
        /// Handles notification pre-processing before a notification handler is invoked.
        /// </summary>
        /// <param name="notification">The type of notification.</param>
        void Handle(TNotification notification);
    }
}