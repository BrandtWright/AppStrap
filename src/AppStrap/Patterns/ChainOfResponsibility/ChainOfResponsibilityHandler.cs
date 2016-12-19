namespace AppStrap.Patterns.ChainOfResponsibility
{
    using System;

    /// <summary>
    /// Default handler for chains of responsibility.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ChainOfResponsibilityHandler<T>
    {
        private readonly IChainResponsibility<T> _chainResponsibility;

        /// <summary>
        /// Initializes a ChainOfResponsibilityHandler class with a IChainResponsibility.
        /// </summary>
        /// <param name="chainResponsibility">A chain of responsibility.</param>
        public ChainOfResponsibilityHandler(IChainResponsibility<T> chainResponsibility)
        {
            if (chainResponsibility == null)
                throw new ArgumentNullException(nameof(chainResponsibility));
            _chainResponsibility = chainResponsibility;
        }

        /// <summary>
        /// Starts at the outer link and gives each link an opportunity to 
        /// handle obj until obj is handled or the entire chain is traversed.
        /// </summary>
        /// <param name="obj"></param>
        public void Handle(T obj)
        {
            foreach (var link in _chainResponsibility.Links)
            {
                if (link.Try(obj)) break;
            }
        }
    }
}