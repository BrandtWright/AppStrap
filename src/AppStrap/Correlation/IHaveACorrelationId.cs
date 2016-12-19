namespace AppStrap.Correlation
{
    using System;

    public interface IHaveACorrelationId
    {
        Guid CorrelationId { get; }
    }
}
