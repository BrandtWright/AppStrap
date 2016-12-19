namespace AppStrap.Queries
{
    using Correlation;
    using Types;

    /// <summary>
    /// Defines a cancellable asynchronous query.
    /// </summary>
    /// <typeparam name="TResponse">The type of response the query will produce.</typeparam>
    public interface ICancellableAsyncQuery<out TResponse> : IHaveACorrelationId { }

    /// <summary>
    /// Defines a cancellable asynchronous query.
    /// </summary>
    public interface ICancellableAsyncQuery : ICancellableAsyncQuery<Unit> { }
}