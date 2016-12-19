namespace AppStrap.Notifications.Internal
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal abstract class CancellableAsyncNotificationHandlerWrapper
    {
        public abstract Task Handle(ICancellableAsyncNotification notification, CancellationToken cancellationToken);
    }

    internal class CancellableAsyncNotificationHandlerWrapper<TNotification> : CancellableAsyncNotificationHandlerWrapper
        where TNotification : ICancellableAsyncNotification
    {
        private readonly IHandleCancellableNotificationsAsynchronously<TNotification> _inner;

        public CancellableAsyncNotificationHandlerWrapper(IHandleCancellableNotificationsAsynchronously<TNotification> inner)
        {
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));
            _inner = inner;
        }

        public override Task Handle(ICancellableAsyncNotification notification, CancellationToken cancellationToken) => 
            _inner.Handle((TNotification) notification, cancellationToken);
    }
}
