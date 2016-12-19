namespace AppStrap.Queries
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a handler that post-processes queries after a query handler has been invoked.
    /// </summary>
    public interface IPostProcessQueries
    {
        /// <summary>
        /// Handles query post-processing.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="result">The query result object.</param>
        void Handle(object query, object result);
    }

    /// <summary>
    /// Defines a handler that post-processes queries after a query handler has been invoked.
    /// </summary>
    /// <typeparam name="TQuery">The type of query.</typeparam>
    /// <typeparam name="TResponse">The type of query result.</typeparam>
    public interface IPostProcessQueries<in TQuery, in TResponse> : IPostProcessQueries
    {
        /// <summary>
        /// Handles query post-processing.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="result">The query result object.</param>
        void Handle(TQuery query, TResponse result);
    }

    /// <summary>
    /// Defines a handler that post-processes queries after a query handler has been invoked.
    /// </summary>
    /// <typeparam name="TQuery">They type of query.</typeparam>
    /// <typeparam name="TResult">The type of query result.</typeparam>
    public abstract class QueryPostProcessor<TQuery, TResult> : IPostProcessQueries<TQuery, TResult>
    {
        /// <summary>
        /// Handles query post-processing.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="result">The query result object.</param>
        public abstract void Handle(TQuery query, TResult result);

        /// <summary>
        /// Handles query post-processing.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="result">The query result object.</param>
        public void Handle(object query, object result)
        {
            if (!(query is TQuery))
                throw new ArgumentException
                    (string.Format("request must be an instance of {0}", query.GetType()), "query");

            if (!(result is TResult))
                throw new ArgumentException(
                    string.Format("result must be an instance of {0}", result.GetType()), "result");

            Handle( (TQuery) query, (TResult) result);
        }
    }

    /// <summary>
    /// Defines a handler that post-processes queries after a query handler has been invoked.
    /// </summary>
    public interface IPostProcessAsyncQueries
    {
        /// <summary>
        /// Handles post-processing of asynchronous queries.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="result">The query result object.</param>
        void Handle(object query, object result);
    }

    /// <summary>
    /// Defines a handler that post-processes queries after a query handler has been invoked.
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IPostProcessAsyncQueries<in TQuery, TResult> : IPostProcessAsyncQueries
    {
        /// <summary>
        /// Handles post-processing of asynchronous queries.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="result">The query result object.</param>
        void Handle(TQuery query, Task<TResult> result);
    }

    /// <summary>
    /// Defines a handler that post-processes queries after a query handler has been invoked.
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class AsyncQueryPostProcessor<TQuery, TResponse> : IPostProcessAsyncQueries<TQuery, TResponse>
    {
        /// <summary>
        /// Handles post-processing of asynchronous queries.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="result">The query result object.</param>
        public abstract void Handle(TQuery query, Task<TResponse> result);

        /// <summary>
        /// Handles post-processing of asynchronous queries.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="result">The query result object.</param>
        public void Handle(object query, object result)
        {
            if (!(query is TQuery))
                throw new ArgumentException
                    (string.Format("request must be an instance of {0}", query.GetType()), "query");

            if (!(result is Task<TResponse>))
                throw new ArgumentException(
                    string.Format("result must be an instance of {0}", result.GetType()), "result");

            Handle((TQuery)query, (Task<TResponse>)result);
        }
    }
}