namespace AppStrap.Queries.Internal
{
    using System;
    using System.Threading.Tasks;

    internal abstract class AsyncQueryHandlerWrapper<TResult>
    {
        public abstract Task<TResult> Handle(IAsyncQuery<TResult> query);
    }

    internal class AsyncQueryHandlerWrapper<TQuery, TResult> : AsyncQueryHandlerWrapper<TResult>
        where TQuery : IAsyncQuery<TResult>
    {
        private readonly IHandleQueriesAsynchronously<TQuery, TResult> _inner;

        public AsyncQueryHandlerWrapper(IHandleQueriesAsynchronously<TQuery, TResult> inner)
        {
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));
            _inner = inner;
        }

        public override Task<TResult> Handle(IAsyncQuery<TResult> query) => _inner.Handle((TQuery) query);
    }
}
