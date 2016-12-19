namespace AppStrap.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a cancellable, asynchronous handler for a command
    /// </summary>
    /// <typeparam name="TCommand">The type of command being handled</typeparam>
    public interface IHandleCancelableCommandsAsynchronously<in TCommand>
        where TCommand : ICancellableAsyncCommand
    {
        /// <summary>
        /// Handles a cancellable, asynchronous command
        /// </summary>
        /// <param name="command">The command object</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>A task representing handling the notification</returns>
        Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}