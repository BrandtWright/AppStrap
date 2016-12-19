namespace AppStrap.SLA
{
    public interface IAgreeOnServiceLevels
    {
        int AcceptableExecutionTime { get; }
    }

    public interface IAgreeOnServiceLevels<T> : IAgreeOnServiceLevels { }
}
