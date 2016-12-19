namespace AppStrap.Notifications
{
    using System;

    /// <summary>
    /// Defines a notification pre-processor.
    /// </summary>
    /// <typeparam name="TNotification">The type of notification.</typeparam>
    public abstract class NotificationPreProcessor<TNotification> : IPreProcessNotifications<TNotification>
    {
        /// <summary>
        /// Handles notification pre-processing before a notification handler is invoked.
        /// </summary>
        /// <param name="notification">The notification object.</param>
        public abstract void Handle(TNotification notification);

        /// <summary>
        /// Handles notification pre-processing before a notification handler is invoked.
        /// </summary>
        /// <param name="notification">The notification object.</param>
        public void Handle(object notification)
        {
            if (!(notification is TNotification))
                throw new ArgumentException(
                    string.Format("Must be an instance of {0}", notification.GetType()), nameof(notification));

            Handle((TNotification)notification);
        }
    }
}