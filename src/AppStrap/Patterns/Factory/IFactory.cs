namespace AppStrap.Patterns.Factory
{
    public interface IFactory<in TIn, out TOut>
    {
        TOut Create(TIn creationalOptions);
    }
}