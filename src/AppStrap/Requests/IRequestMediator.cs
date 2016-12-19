namespace AppStrap.Requests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Commands;
    using Notifications;
    using Queries;

    /// <summary>
    /// Defines a mediator to encapsulate request/response and publishing patterns
    /// </summary>
    public interface IRequestMediator
    {
        // Queries
        TResult Send<TResult>(IQuery<TResult> query);
        Task<TResult> SendAsync<TResult>(IAsyncQuery<TResult> query);
        Task<TResult> SendAsync<TResult>(ICancellableAsyncQuery<TResult> query, CancellationToken cancellationToken);

        // Commands
        void Execute(ICommand command);
        Task ExecuteAsync(IAsyncCommand command);
        Task ExecuteAsync(ICancellableAsyncCommand command, CancellationToken cancellationToken);

        // Notifications
        void Publish(INotification notification);
        Task PublishAsync(IAsyncNotification notification);
        Task PublishAsync(ICancellableAsyncNotification notification, CancellationToken cancellationToken);
    }
}