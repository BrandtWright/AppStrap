namespace AppStrap.Queries
{
    using System;

    /// <summary>
    /// Defines a query pre-processor that handles queries before a query handler has been invoked.
    /// </summary>
    public interface IPreProcessQueries
    {
        /// <summary>
        /// Handles query pre-processing.
        /// </summary>
        /// <param name="query">The query object.</param>
        void Handle(object query);
    }

    /// <summary>
    /// Defines a query pre-processof that handles queries before a query handler has been invoked.
    /// </summary>
    /// <typeparam name="TQuery">The type of query.</typeparam>
    public interface IPreProcessQueries<in TQuery> : IPreProcessQueries
    {
        /// <summary>
        /// Handles query pre-processing.
        /// </summary>
        /// <param name="query">The query object.</param>
        void Handle(TQuery query);
    }

    /// <summary>
    /// Defines a query pre-processof that handles queries before a query handler has been invoked.
    /// </summary>
    /// <typeparam name="TQuery">The type of query.</typeparam>
    public abstract class QueryPreProcessor<TQuery> : IPreProcessQueries<TQuery>
    {
        /// <summary>
        /// Handles query pre-processing.
        /// </summary>
        /// <param name="query">The query object.</param>
        public abstract void Handle(TQuery query);

        /// <summary>
        /// Handles query pre-processing.
        /// </summary>
        /// <param name="query">The query object.</param>
        public void Handle(object query)
        {
            if (!(query is TQuery))
                throw new ArgumentException(
                    string.Format("Must be an instance of {0}", query.GetType()), "query");

            Handle((TQuery) query);
        }
    }
}