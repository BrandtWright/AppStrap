namespace AppStrap.Notifications
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a mediator to encapsulate request/response publishing patterns.
    /// </summary>
    public interface INotificationMediator
    {
        /// <summary>
        /// Sends a notification to multiple handlers.
        /// </summary>
        /// <param name="notification">A notification object.</param>
        void Publish(INotification notification);

        /// <summary>
        /// Asynchronously sends a notification to multiple handlers.
        /// </summary>
        /// <param name="notification">An asynchronous notification object.</param>
        /// <returns></returns>
        Task PublishAsync(IAsyncNotification notification);

        /// <summary>
        /// Asynchronously sends a notification to multiple handlers.
        /// </summary>
        /// <param name="notification">A cancellable asynchronous notification object.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishAsync(ICancellableAsyncNotification notification, CancellationToken cancellationToken);
    }
}