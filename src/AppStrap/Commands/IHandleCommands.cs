namespace AppStrap.Commands
{
    /// <summary>
    /// Defines a handler for a command.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface IHandleCommands<in TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// Handles a command.
        /// </summary>
        /// <param name="command">The command object.</param>
        void Handle(TCommand command);
    }
}