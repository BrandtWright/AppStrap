namespace AppStrap.Queries.Internal
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal abstract class CancellableAsyncQueryHandlerWrapper<TResult>
    {
        public abstract Task<TResult> Handle(ICancellableAsyncQuery<TResult> query, CancellationToken cancellationToken);
    }

    internal class CancellableAsyncQueryHandlerWrapper<TQuery, TResult> : CancellableAsyncQueryHandlerWrapper<TResult>
        where TQuery : ICancellableAsyncQuery<TResult>
    {
        private readonly IHandleCancellableQueriesAsynchronously<TQuery, TResult> _inner;

        public CancellableAsyncQueryHandlerWrapper(IHandleCancellableQueriesAsynchronously<TQuery, TResult> inner)
        {
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));
            _inner = inner;
        }

        public override Task<TResult> Handle(ICancellableAsyncQuery<TResult> query, CancellationToken cancellationToken) => 
            _inner.Handle((TQuery) query, cancellationToken);
    }
}
