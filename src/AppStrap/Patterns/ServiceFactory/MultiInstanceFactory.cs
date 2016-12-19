namespace AppStrap.Patterns.ServiceFactory
{
    using System;
    using System.Collections.Generic;

    public delegate IEnumerable<object> MultiInstanceFactory(Type serviceType);
}
