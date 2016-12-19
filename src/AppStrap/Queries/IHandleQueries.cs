namespace AppStrap.Queries
{
    /// <summary>
    /// Defines a handler for a query.
    /// </summary>
    /// <typeparam name="TQuery">The type of query.</typeparam>
    /// <typeparam name="TResult">The type of query result.</typeparam>
    public interface IHandleQueries<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Handles a query.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <returns>The query result.</returns>
        TResult Handle(TQuery query);
    }
}