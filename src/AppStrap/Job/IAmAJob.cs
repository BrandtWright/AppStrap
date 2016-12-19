namespace AppStrap.Job
{
    using Commands;

    public interface IAmAJob : ICommand
    {
        string Name { get;  }
        string Description { get; }
    }

    public interface IAmAnAsyncJob : IAsyncCommand
    {
        string Name { get; }
        string Description { get; }
    }

    public interface IAmACancellableAsyncJob : ICancellableAsyncCommand
    {
        string Name { get; }
        string Description { get; }
    }
}
