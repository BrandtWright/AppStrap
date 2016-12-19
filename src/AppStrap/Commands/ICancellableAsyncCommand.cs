namespace AppStrap.Commands
{
    using Correlation;

    /// <summary>
    /// Defines a cancellable asynchronous command.
    /// </summary>
    public interface ICancellableAsyncCommand : IHaveACorrelationId { }
}