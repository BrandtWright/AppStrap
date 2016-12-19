namespace AppStrap.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a mediator to encapsulate command processessing patterns.
    /// </summary>
    public interface ICommandMediator
    {
        /// <summary>
        /// Executes a command synchronously.
        /// </summary>
        /// <param name="command">The command object.</param>
        void Execute(ICommand command);
        
        /// <summary>
        /// Executes a command asynchronously.
        /// </summary>
        /// <param name="command">The command object.</param>
        /// <returns></returns>
        Task ExecuteAsync(IAsyncCommand command);
        
        /// <summary>
        /// Executes a cancellable command asynchronously.
        /// </summary>
        /// <param name="command">The command object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task ExecuteAsync(ICancellableAsyncCommand command, CancellationToken cancellationToken);
    }
}