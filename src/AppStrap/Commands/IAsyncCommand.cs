namespace AppStrap.Commands
{
    using Correlation;

    /// <summary>
    /// Defines an asynchronous command.
    /// </summary>
    public interface IAsyncCommand : IHaveACorrelationId { }
}