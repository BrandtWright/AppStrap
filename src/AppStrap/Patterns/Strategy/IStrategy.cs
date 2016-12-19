namespace AppStrap.Patterns.Strategy
{
    public interface IStrategy<in TIn, out TOut>
    {
        TOut Compute(TIn obj);
    }
}