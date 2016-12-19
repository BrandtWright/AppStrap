namespace AppStrap.Queries
{
    using Correlation;

    /// <summary>
    /// Defines a query.
    /// </summary>
    /// <typeparam name="TResult">The type of result that will be produced.</typeparam>
    public interface IQuery<TResult> : IHaveACorrelationId { }
}
