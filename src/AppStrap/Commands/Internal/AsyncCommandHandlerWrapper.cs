namespace AppStrap.Commands.Internal
{
    using System;
    using System.Threading.Tasks;

    internal abstract class AsyncCommandHandlerWrapper
    {
        public abstract Task Handle(IAsyncCommand command);
    }

    internal class AsyncCommandHandlerWrapper<TCommand> : AsyncCommandHandlerWrapper
        where TCommand : IAsyncCommand
    {
        private readonly IHandleCommandsAsynchronously<TCommand> _inner;

        public AsyncCommandHandlerWrapper(IHandleCommandsAsynchronously<TCommand> inner)
        {
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));
            _inner = inner;
        }

        public override Task Handle(IAsyncCommand command) => 
            _inner.Handle((TCommand) command);
    }
}
