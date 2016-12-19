namespace AppStrap.Notifications
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Internal;
    using Patterns.ServiceFactory;
    /// <summary>
    /// Default notification mediator relying on single and multi instance delegates for resolving handlers.
    /// </summary>
    public class NotificationMediator : INotificationMediator
    {
        #region Private Fields

        private readonly MultiInstanceFactory _multiInstanceFactory;
        private readonly ConcurrentDictionary<Type, Type> _genericHandlerCache;
        private readonly ConcurrentDictionary<Type, Type> _wrapperHandlerCache;

        #endregion Private Fields

        #region Constructors

        public NotificationMediator(SingleInstanceFactory singleInstanceFactory)
            : this(t => (IEnumerable<object>)singleInstanceFactory(typeof(IEnumerable<>).MakeGenericType(t))) { }

        public NotificationMediator(MultiInstanceFactory multiInstanceFactory)
        {
            if (multiInstanceFactory == null)
                throw new ArgumentNullException(nameof(multiInstanceFactory));

            _multiInstanceFactory = multiInstanceFactory;
            _genericHandlerCache = new ConcurrentDictionary<Type, Type>();
            _wrapperHandlerCache = new ConcurrentDictionary<Type, Type>();
        }

        #endregion Constructors

        #region Notifications

        public void Publish(INotification notification)
        {
            var notificationHandlers = GetNotificationsHandlers(notification);

            foreach (var handler in notificationHandlers)
            {
                handler.Handle(notification);
            }
        }

        private IEnumerable<NotificationHandlerWrapper> GetNotificationsHandlers(INotification notification) => 
            GetNotificationHandlers<NotificationHandlerWrapper>(
                notification,
                typeof (IHandleNotifications<>),
                typeof (NotificationHandlerWrapper<>)
            );

        #endregion Notifications

        #region Async Notifications

        public async Task PublishAsync(IAsyncNotification notification)
        {
            var notificationHandlers = GetNotificationHandlers(notification)
                .Select(handler => handler.Handle(notification))
                .ToArray();

            await Task.WhenAll(notificationHandlers);
        }

        private IEnumerable<AsyncNotificationHandlerWrapper> GetNotificationHandlers(IAsyncNotification notification) => 
            GetNotificationHandlers<AsyncNotificationHandlerWrapper>(
                notification,
                typeof(IHandleNotificationsAsynchronously<>),
                typeof(AsyncNotificationHandlerWrapper<>)
            );

        #endregion Async Notifications

        #region Cancellable Async Notifications

        public async Task PublishAsync(ICancellableAsyncNotification notification, CancellationToken cancellationToken)
        {
            var notificationHandlers = GetNotificationHandlers(notification)
                .Select(handler => handler.Handle(notification, cancellationToken))
                .ToArray();

            await Task.WhenAll(notificationHandlers);
        }

        private IEnumerable<CancellableAsyncNotificationHandlerWrapper> GetNotificationHandlers(ICancellableAsyncNotification notification) => 
            GetNotificationHandlers<CancellableAsyncNotificationHandlerWrapper>(
                notification,
                typeof(IHandleCancellableNotificationsAsynchronously<>),
                typeof(CancellableAsyncNotificationHandlerWrapper)
            );

        #endregion Cancellable Async Notifications
        
        #region Common

        private IEnumerable<TWrapper> GetNotificationHandlers<TWrapper>(object notification, Type handlerType, Type wrapperType)
        {
            var notificationType = notification.GetType();

            var genericHandlerType = _genericHandlerCache.GetOrAdd(notificationType,
                type => handlerType.MakeGenericType(notificationType));

            var genericWrapperType = _wrapperHandlerCache.GetOrAdd(notificationType,
                type => wrapperType.MakeGenericType(notificationType));

            return GetNotificationHandlers(notification, genericHandlerType)
                .Select(handler => Activator.CreateInstance(genericWrapperType, handler))
                .Cast<TWrapper>()
                .ToList();
        }

        private IEnumerable<object> GetNotificationHandlers(object notification, Type handlerType)
        {
            try
            {
                return _multiInstanceFactory(handlerType);
            }
            catch (Exception e)
            {
                throw BuildException(notification, e);
            }
        }

        private static InvalidOperationException BuildException(object notification, Exception inner = null) => 
            new InvalidOperationException(
                "Handler was not found for request of type " + 
                notification.GetType() + 
                ".\r\nContainer or service locator not configured properly or handlers not registered with your container.", 
                inner
            );

        #endregion Common
    }
}