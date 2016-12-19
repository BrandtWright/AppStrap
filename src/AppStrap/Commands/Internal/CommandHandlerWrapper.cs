namespace AppStrap.Commands.Internal
{
    using System;

    internal abstract class CommandHandlerWrapper
    {
        public abstract void Handle(ICommand command);
    }

    internal class CommandHandlerWrapper<TCommand> : CommandHandlerWrapper
        where TCommand : ICommand
    {
        private readonly IHandleCommands<TCommand> _inner;

        public CommandHandlerWrapper(IHandleCommands<TCommand> inner)
        {
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));
            _inner = inner;
        }

        public override void Handle(ICommand command)
        {
            _inner.Handle((TCommand) command);
        }
    }
}
