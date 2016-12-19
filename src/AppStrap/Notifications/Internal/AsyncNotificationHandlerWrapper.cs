namespace AppStrap.Notifications.Internal
{
    using System;
    using System.Threading.Tasks;

    internal abstract class AsyncNotificationHandlerWrapper
    {
        public abstract Task Handle(IAsyncNotification notification);
    }

    internal class AsyncNotificationHandlerWrapper<TNotification> : AsyncNotificationHandlerWrapper
        where TNotification : IAsyncNotification
    {
        private readonly IHandleNotificationsAsynchronously<TNotification> _inner;

        public AsyncNotificationHandlerWrapper(IHandleNotificationsAsynchronously<TNotification> inner)
        {
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));
            _inner = inner;
        }
        public override Task Handle(IAsyncNotification notification) => _inner.Handle((TNotification) notification);
    }
}
