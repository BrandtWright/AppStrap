namespace AppStrap.Queries
{
    using Correlation;

    /// <summary>
    /// Defines an asynchronous query.
    /// </summary>
    /// <typeparam name="TResult">The type of result the query will return.</typeparam>
    public interface IAsyncQuery<TResult> : IHaveACorrelationId { }
}