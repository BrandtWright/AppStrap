namespace AppStrap.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a cancellable, asynchronous handler for a query.
    /// </summary>
    /// <typeparam name="TQuery">The type of query being handled</typeparam>
    /// <typeparam name="TResult">The type of query result the query will produce.</typeparam>
    public interface IHandleCancellableQueriesAsynchronously<in TQuery, TResult>
        where TQuery : ICancellableAsyncQuery<TResult>
    {
        /// <summary>
        /// Defines a cancellable asynchronous handler for a query
        /// </summary>
        /// <param name="query">The query object</param>
        /// <param name="cancellationToken">A cancellation token</param>
        /// <returns>A task representing the response from the request</returns>
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
    }
}