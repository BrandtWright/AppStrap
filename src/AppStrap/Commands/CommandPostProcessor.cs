namespace AppStrap.Commands
{
    using System;

    /// <summary>
    /// Defines a command post-processor.
    /// </summary>
    public interface IPostProcessCommands
    {
        /// <summary>
        /// Handles command post-processing after a command handler has been invoked.
        /// </summary>
        /// <param name="command"></param>
        void Handle(object command);
    }

    /// <summary>
    /// Defines a command post-processor.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to be post-processed.</typeparam>
    public interface IPostProcessCommands<in TCommand> : IPostProcessCommands
    {
        /// <summary>
        /// Handles command post-processing after a command handler has been invoked.
        /// </summary>
        /// <param name="command">The command object.</param>
        void Handle(TCommand command);
    }

    /// <summary>
    /// Defines a command post-processor.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to be post-processed.</typeparam>
    public abstract class CommandPostProcessor<TCommand> : IPostProcessCommands<TCommand>
    {
        /// <summary>
        /// Handles command post-processing after a command handler has been invoked.
        /// </summary>
        /// <param name="command">The command object.</param>
        public abstract void Handle(TCommand command);

        /// <summary>
        /// Handles command post-processing after a command handler has been invoked.
        /// </summary>
        /// <param name="command">The command object.</param>
        public void Handle(object command)
        {
            if (!(command is TCommand))
                throw new ArgumentException(
                    string.Format("Must be an instance of {0}", command.GetType()), nameof(command));

            Handle((TCommand)command);
        }
    }
}