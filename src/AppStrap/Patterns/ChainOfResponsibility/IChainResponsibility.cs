namespace AppStrap.Patterns.ChainOfResponsibility
{
    using System.Collections.Generic;
    using TesterDoer;

    public interface IChainResponsibility<T> 
    {
       IEnumerable<TesterDoer<T>> Links { get; }
    }
}
