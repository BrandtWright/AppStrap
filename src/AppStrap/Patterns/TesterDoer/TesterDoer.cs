namespace AppStrap.Patterns.TesterDoer
{
    using System;

    public class TesterDoer<T>
    {
        private readonly Predicate<T> _tester;
        private readonly Action<T> _doer;

        /// <summary>
        /// Initializes a new instance of the TesterDoer class with the test 
        /// criteria and the action to invoke if the criteria resolves to true.
        /// </summary>
        /// <param name="tester">The test criteria.</param>
        /// <param name="doer">The action to perform if the test criteria evaluates to true.</param>
        public TesterDoer(Predicate<T> tester, Action<T> doer)
        {
            if (tester == null) throw new ArgumentNullException(nameof(tester));
            if (doer == null) throw new ArgumentNullException(nameof(doer));
            _tester = tester;
            _doer = doer;
        }

        /// <summary>
        /// Performs the test and invokes the action if test is successfull.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Try(T obj)
        {
            if (!_tester(obj)) return false;
            _doer.Invoke(obj);
            return true;
        }
    }
}