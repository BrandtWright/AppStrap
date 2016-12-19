namespace AppStrap.Commands
{
    using System;

    /// <summary>
    /// Defines a command pre-processor.
    /// </summary>
    public interface IPreProcessCommands
    {
        /// <summary>
        /// Handles command pre-processing before a command handler is invoked.
        /// </summary>
        /// <param name="command">The command object.</param>
        void Handle(object command);
    }

    /// <summary>
    /// Defines a command pre-processor.
    /// </summary>
    /// <typeparam name="TCommand">The type of command object.</typeparam>
    public interface IPreProcessCommands<in TCommand> : IPreProcessCommands
    {
        /// <summary>
        /// Handles command pre-processing before a command handler is invoked.
        /// </summary>
        /// <param name="command">The command object</param>
        void Handle(TCommand command);
    }

    /// <summary>
    /// Defines a command pre-processor.
    /// </summary>
    /// <typeparam name="TCommand">The type of command object.</typeparam>
    public abstract class CommandPreProcessor<TCommand> : IPreProcessCommands<TCommand>
    {
        /// <summary>
        ///  Handles command pre-processing before a command handler is invoked.
        /// </summary>
        /// <param name="command">The command object</param>
        public abstract void Handle(TCommand command);
        
        /// <summary>
        /// Handles command pre-processing before a command handler is invoked.
        /// </summary>
        /// <param name="command">The command object.</param>
        public void Handle(object command)
        {
            if (!(command is TCommand))
                throw new ArgumentException(
                    string.Format("Must be an instance of {0}", command.GetType()), "command");

            Handle((TCommand) command);
        }
    }
}