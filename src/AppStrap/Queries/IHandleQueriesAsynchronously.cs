namespace AppStrap.Queries
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an asynchronous query handler.
    /// </summary>
    /// <typeparam name="TQuery">The type of query.</typeparam>
    /// <typeparam name="TResult">The type of query result.</typeparam>
    public interface IHandleQueriesAsynchronously<in TQuery, TResult> where TQuery : IAsyncQuery<TResult>
    {
        /// <summary>
        /// Handles an query asynchronously.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <returns>A task of query result.</returns>
        Task<TResult> Handle(TQuery query);
    }
}