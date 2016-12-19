namespace AppStrap.Patterns.Specification
{
    using System;

    internal class NotSpecification<TEntity> : ISpecification<TEntity>
    {
        protected ISpecification<TEntity> Wrapped { get; }

        internal NotSpecification(ISpecification<TEntity> spec)
        {
            if (spec == null)
                throw new ArgumentNullException(nameof(spec));

            Wrapped = spec;
        }

        public bool IsSatisfiedBy(TEntity candidate) => !Wrapped.IsSatisfiedBy(candidate);
    }
}