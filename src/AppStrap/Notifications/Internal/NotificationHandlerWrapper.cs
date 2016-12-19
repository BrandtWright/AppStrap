namespace AppStrap.Notifications.Internal
{
    using System;

    internal abstract class NotificationHandlerWrapper
    {
        public abstract void Handle(INotification notification);
    }

    internal class NotificationHandlerWrapper<TNotification> : NotificationHandlerWrapper
        where TNotification : INotification
    {
        private readonly IHandleNotifications<TNotification> _inner;

        public NotificationHandlerWrapper(IHandleNotifications<TNotification> inner)
        {
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));
            _inner = inner;
        }
        

        public override void Handle(INotification notification) => 
            _inner.Handle((TNotification) notification);
    }
}
