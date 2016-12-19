namespace AppStrap.Notifications
{
    using System;


    /// <summary>
    /// Defines a notification post-processor.
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public abstract class NotificationPostProcessor<TNotification> : IPostProcessNotifications<TNotification>
    {
        /// <summary>
        /// Handles notification post-processing before a notification handler is invoked.
        /// </summary>
        /// <param name="notification">The notification object..</param>
        public abstract void Handle(TNotification notification);

        /// <summary>
        /// Handles notification post-processing before a notification handler is invoked.
        /// </summary>
        /// <param name="notification">The notification object..</param>
        public void Handle(object notification)
        {
            if (!(notification is TNotification))
                throw new ArgumentException(
                    string.Format("Must be an instance of {0}", notification.GetType()), nameof(notification));

            Handle((TNotification)notification);
        }
    }
}