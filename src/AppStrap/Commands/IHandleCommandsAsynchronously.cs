namespace AppStrap.Commands
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an asynchronous handler for a command.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface IHandleCommandsAsynchronously<in TCommand>
        where TCommand : IAsyncCommand
    {
        /// <summary>
        /// Handles an asynchronous command.
        /// </summary>
        /// <param name="command">The command object.</param>
        /// <returns></returns>
        Task Handle(TCommand command);
    }
}