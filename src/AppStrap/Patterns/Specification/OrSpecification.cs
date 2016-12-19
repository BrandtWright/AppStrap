namespace AppStrap.Patterns.Specification
{
    using System;

    internal class OrSpecification<TEntity> : ISpecification<TEntity>
    {
        protected ISpecification<TEntity> Spec1 { get; }

        protected ISpecification<TEntity> Spec2 { get; }

        internal OrSpecification(ISpecification<TEntity> spec1, ISpecification<TEntity> spec2)
        {
            if (spec1 == null)
                throw new ArgumentNullException(nameof(spec1));

            if (spec2 == null)
                throw new ArgumentNullException(nameof(spec2));

            Spec1 = spec1;
            Spec2 = spec2;
        }

        public bool IsSatisfiedBy(TEntity candidate) => 
            Spec1.IsSatisfiedBy(candidate) || Spec2.IsSatisfiedBy(candidate);
    }
}