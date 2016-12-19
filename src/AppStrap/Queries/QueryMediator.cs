namespace AppStrap.Queries
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using Internal;
    using Patterns.ServiceFactory;

    /// <summary>
    /// Default query mediator relying on single and multi instance delegates for resolving handlers.
    /// </summary>
    public class QueryMediator : IQueryMediator
    {

        #region Fields

        private readonly SingleInstanceFactory _singleInstanceFactory;
        private readonly ConcurrentDictionary<Type, Type> _genericHandlerCache;
        private readonly ConcurrentDictionary<Type, Type> _wrapperHandlerCache;

        #endregion Fields

        #region Constructors

        public QueryMediator(SingleInstanceFactory singleInstanceFactory)
        {
            if (singleInstanceFactory == null)
                throw new ArgumentNullException(nameof(singleInstanceFactory));

            _singleInstanceFactory = singleInstanceFactory;
            _genericHandlerCache = new ConcurrentDictionary<Type, Type>();
            _wrapperHandlerCache = new ConcurrentDictionary<Type, Type>();
        }

        #endregion Constructors

        #region Exception Handlers

        private static InvalidOperationException BuildException(object message, Exception inner = null) => 
            new InvalidOperationException(
                "Handler was not found for request of type " + 
                message.GetType() + 
                ".\r\nContainer or service locator not configured properly or handlers not registered with your container.", 
                inner
            );

        #endregion Exception Handlers

        #region Query

        public TResult Send<TResult>(IQuery<TResult> query)
        {
            var defaultHandler = GetHandler(query);
            var result = defaultHandler.Handle(query);
            return result;
        }

        QueryHandlerWrapper<TResult> GetHandler<TResult>(IQuery<TResult> query) => 
            GetHandler<QueryHandlerWrapper<TResult>, TResult>(
                query, typeof (IHandleQueries<,>), typeof (QueryHandlerWrapper<,>)
            );

        #endregion Query

        #region Async Query

        public async Task<TResult> SendAsync<TResult>(IAsyncQuery<TResult> query)
        {
            var defaultHandler = GetHandler(query);
            var result = await defaultHandler.Handle(query);
            return result;
        }

        private AsyncQueryHandlerWrapper<TResult> GetHandler<TResult>(IAsyncQuery<TResult> query) => 
            GetHandler<AsyncQueryHandlerWrapper<TResult>, TResult>(
                query, typeof(IHandleQueriesAsynchronously<,>), typeof(AsyncQueryHandlerWrapper<,>));

        #endregion Async Query

        #region Cancellable Async Query

        public async Task<TResult> SendAsync<TResult>(ICancellableAsyncQuery<TResult> query, CancellationToken cancellationToken)
        {
            var handler = GetHandler(query);
            var result = await handler.Handle(query, cancellationToken);
            return result;
        }

        private CancellableAsyncQueryHandlerWrapper<TResult> GetHandler<TResult>(ICancellableAsyncQuery<TResult> query) => 
            GetHandler<CancellableAsyncQueryHandlerWrapper<TResult>, TResult>(
                query, typeof (IHandleCancellableQueriesAsynchronously<,>), typeof (CancellableAsyncQueryHandlerWrapper<,>));

        #endregion Cancellable Async Query

        #region Common

        private TWrapper GetHandler<TWrapper, TResponse>(object query, Type handlerType, Type wrapperType)
        {
            var queryType = query.GetType();

            var genericHandlerType = _genericHandlerCache.GetOrAdd(queryType,
                type => handlerType.MakeGenericType(queryType, typeof (TResponse)));

            var genericWrapperType = _wrapperHandlerCache.GetOrAdd(queryType,
                type => wrapperType.MakeGenericType(queryType, typeof (TResponse)));
            
            var handler = GetHandler(query, genericHandlerType);

            if (handler is Array)
                handler = ((Array) handler).GetValue(0);

            return (TWrapper)Activator.CreateInstance(genericWrapperType, handler);
        }

        private object GetHandler(object query, Type handlerType)
        {
            try
            {
                return _singleInstanceFactory(handlerType);
            }
            catch (Exception e)
            {
                throw BuildException(query, e);
            }
        }

        #endregion Common
    }
}