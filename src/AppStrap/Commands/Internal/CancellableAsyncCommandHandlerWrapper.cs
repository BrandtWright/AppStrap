namespace AppStrap.Commands.Internal
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal abstract class CancellableAsyncCommandHandlerWrapper
    {
        public abstract Task Handle(ICancellableAsyncCommand command, CancellationToken cancellationToken);
    }

    internal class CancellableAsyncCommandHandlerWrapper<TCommand> : CancellableAsyncCommandHandlerWrapper
        where TCommand : ICancellableAsyncCommand
    {
        private readonly IHandleCancelableCommandsAsynchronously<TCommand> _inner;

        public CancellableAsyncCommandHandlerWrapper(IHandleCancelableCommandsAsynchronously<TCommand> inner)
        {
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));
            _inner = inner;
        }

        public override Task Handle(ICancellableAsyncCommand command, CancellationToken cancellationToken) => 
            _inner.Handle((TCommand) command, cancellationToken);
    }
}
