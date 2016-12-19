namespace AppStrap.Patterns.Strategy
{
    using System;

    public abstract class Strategy<TIn, TOut> : IStrategy<TIn, TOut>
    {
        private readonly Func<TIn, TOut> _strategy;

        protected Strategy(Func<TIn, TOut> strategy)
        {
            if (strategy == null)
                throw new ArgumentNullException(nameof(strategy));
            _strategy = strategy;
        }

        public TOut Compute(TIn obj) { return _strategy.Invoke(obj); }
    }
}
