namespace AppStrap.Patterns.Factory
{
    using System;

    public abstract class FactoryBase<TIn,TOut> : IFactory<TIn, TOut>
    {
        private readonly Func<TIn, TOut> _creationalLogic;

        protected FactoryBase(Func<TIn, TOut> creationalLogic)
        {
            if (creationalLogic == null)
                throw new ArgumentNullException(nameof(creationalLogic));
            _creationalLogic = creationalLogic;
        }

        public TOut Create(TIn creationalOptions) => _creationalLogic.Invoke(creationalOptions);
    }
}