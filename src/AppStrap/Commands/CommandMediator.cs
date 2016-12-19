namespace AppStrap.Commands
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using Internal;
    using Patterns.ServiceFactory;

    public class CommandMediator : ICommandMediator
    {
        #region Fields

        private readonly SingleInstanceFactory _singleInstanceFactory;
        private readonly ConcurrentDictionary<Type, Type> _genericHandlerCache;
        private readonly ConcurrentDictionary<Type, Type> _wrapperHandlerCache;

        #endregion Fields

        #region Constructors

        public CommandMediator(SingleInstanceFactory singleInstanceFactory)
        {
            _singleInstanceFactory = singleInstanceFactory;
            _genericHandlerCache = new ConcurrentDictionary<Type, Type>();
            _wrapperHandlerCache = new ConcurrentDictionary<Type, Type>();
        }

        #endregion Constructors

        #region Exception Handlers

        private static InvalidOperationException BuildException(Type type, Exception inner = null) => new InvalidOperationException(
            "Handler was not found for ICommand of type " + 
            type + 
            ".\r\nContainer or service locator not configured properly or handlers not registered with your container.", 
            inner
        );

        #endregion Exception Handlers

        #region Commands

        public void Execute(ICommand command) => GetCommandHandler(command).Handle(command);

        private CommandHandlerWrapper GetCommandHandler(ICommand command) => 
            GetCommandHandler<CommandHandlerWrapper>(
                command,
                typeof (IHandleCommands<>),
                typeof (CommandHandlerWrapper<>)
            );

        #endregion Commands

        #region Async Commands

        public async Task ExecuteAsync(IAsyncCommand command)
        {
            var handler = GetCommandHandler(command);
            await handler.Handle(command);
        }

        private AsyncCommandHandlerWrapper GetCommandHandler(IAsyncCommand command) => 
            GetCommandHandler<AsyncCommandHandlerWrapper>(
                command,
                typeof (IHandleCommandsAsynchronously<>),
                typeof (AsyncCommandHandlerWrapper<>)
            );

        #endregion Async Commands

        #region Cancellable Async Commands

        public async Task ExecuteAsync(ICancellableAsyncCommand command, CancellationToken cancellationToken)
        {
            var handler = GetCommandHandler(command);
            await handler.Handle(command, cancellationToken);
        }

        private CancellableAsyncCommandHandlerWrapper GetCommandHandler(ICancellableAsyncCommand command) => 
            GetCommandHandler<CancellableAsyncCommandHandlerWrapper>(
                command,
                typeof (IHandleCancelableCommandsAsynchronously<>),
                typeof (CancellableAsyncCommandHandlerWrapper<>)
            );

        #endregion Cancellable Async Commands

        #region Common

        private TWrapper GetCommandHandler<TWrapper>(object command, Type handlerType, Type wrapperType)
        {
            var commandType = command.GetType();

            var genericHandlerType = _genericHandlerCache.GetOrAdd(commandType,
                type => handlerType.MakeGenericType(commandType));

            var genericWrapperType = _wrapperHandlerCache.GetOrAdd(commandType,
                type => wrapperType.MakeGenericType(commandType));
            
            var handler = GetCommandHandler(command, genericHandlerType);

            return (TWrapper)Activator.CreateInstance(genericWrapperType, handler);
        }

        private object GetCommandHandler(object command, Type handlerType)
        {
            try
            {
                return _singleInstanceFactory(handlerType);
            }
            catch (Exception e)
            {
                throw BuildException(command, e);
            }
        }

        private static InvalidOperationException BuildException(object command, Exception inner = null) => 
            new InvalidOperationException(
                "Handler was not found for request of type " + 
                command.GetType() + 
                ".\r\nContainer or service locator not configured properly or handlers not registered with your container.", 
                inner
            );

        #endregion Common
    }
}