namespace AppStrap.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a mediator to encapsulate query request / query response interaction patterns.
    /// </summary>
    public interface IQueryMediator
    {
        /// <summary>
        /// Sends a query to a single query handler.
        /// </summary>
        /// <typeparam name="TResult">The type of query result that will be produced.</typeparam>
        /// <param name="query">The query object.</param>
        /// <returns>A query result.</returns>
        TResult Send<TResult>(IQuery<TResult> query);

        /// <summary>
        /// Asynchronously sends a query to a single query handler.
        /// </summary>
        /// <typeparam name="TResult">The type of result that will be produced.</typeparam>
        /// <param name="query">The query object.</param>
        /// <returns>A task of query result.</returns>
        Task<TResult> SendAsync<TResult>(IAsyncQuery<TResult> query);

        /// <summary>
        /// Asynchronously sends a cancellable query to a single query handler.
        /// </summary>
        /// <typeparam name="TResult">The type of result that will be produced.</typeparam>
        /// <param name="query">The query object.</param>
        /// <param name="cancellationToken">A token used for cancellation purposes.</param>
        /// <returns>A task of query result.</returns>
        Task<TResult> SendAsync<TResult>(ICancellableAsyncQuery<TResult> query, CancellationToken cancellationToken);
    }
}