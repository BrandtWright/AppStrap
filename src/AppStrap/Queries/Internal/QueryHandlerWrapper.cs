namespace AppStrap.Queries.Internal
{
    using System;

    internal abstract class QueryHandlerWrapper<TResult>
    {
        public abstract TResult Handle(IQuery<TResult> query);
    }

    internal class QueryHandlerWrapper<TQuery, TResult> : QueryHandlerWrapper<TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IHandleQueries<TQuery, TResult> _inner;

        public QueryHandlerWrapper(IHandleQueries<TQuery, TResult> inner)
        {
            if (inner == null)
                throw new ArgumentNullException(nameof(inner));
            _inner = inner;
        }

        public override TResult Handle(IQuery<TResult> query) => _inner.Handle((TQuery)query);
    }
}
