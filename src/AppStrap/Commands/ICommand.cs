namespace AppStrap.Commands
{
    using Correlation;

    /// <summary>
    /// Defines a command.
    /// </summary>
    public interface ICommand : IHaveACorrelationId { }
}
