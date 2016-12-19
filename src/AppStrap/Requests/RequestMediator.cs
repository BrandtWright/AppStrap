namespace AppStrap.Requests
{
    using System.Threading;
    using Commands;
    using Notifications;
    using Queries;
    using System;
    using System.Threading.Tasks;

    public class RequestMediator : IRequestMediator
    {
        private readonly IQueryMediator _queryMediator;
        private readonly ICommandMediator _commandMediator;
        private readonly INotificationMediator _notificationMediator;

        public RequestMediator(
            IQueryMediator queryMediator,
            ICommandMediator commandMediator,
            INotificationMediator notificationMediator)
        {
            if (queryMediator == null)
                throw new ArgumentNullException(nameof(queryMediator));

            if (commandMediator == null)
                throw new ArgumentNullException(nameof(commandMediator));

            if (notificationMediator == null)
                throw new ArgumentNullException(nameof(notificationMediator));

            _queryMediator = queryMediator;
            _commandMediator = commandMediator;
            _notificationMediator = notificationMediator;
        }

        public TResult Send<TResult>(IQuery<TResult> query) =>
            _queryMediator.Send(query);

        public Task<TResult> SendAsync<TResult>(IAsyncQuery<TResult> query) =>
            _queryMediator.SendAsync(query);

        public Task<TResult> SendAsync<TResult>(ICancellableAsyncQuery<TResult> query, CancellationToken cancellationToken) => 
            _queryMediator.SendAsync(query, cancellationToken);

        public void Execute(ICommand command) =>
            _commandMediator.Execute(command);

        public Task ExecuteAsync(IAsyncCommand command) =>
            _commandMediator.ExecuteAsync(command);

        public Task ExecuteAsync(ICancellableAsyncCommand command, CancellationToken cancellationToken) => 
            _commandMediator.ExecuteAsync(command, cancellationToken);

        public void Publish(INotification notification) =>
            _notificationMediator.Publish(notification);

        public Task PublishAsync(IAsyncNotification notification) => 
            _notificationMediator.PublishAsync(notification);

        public Task PublishAsync(ICancellableAsyncNotification notification, CancellationToken cancellationToken) => 
            _notificationMediator.PublishAsync(notification, cancellationToken);
    }
}
