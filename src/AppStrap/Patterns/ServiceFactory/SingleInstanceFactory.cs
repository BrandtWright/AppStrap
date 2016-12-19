namespace AppStrap.Patterns.ServiceFactory
{
    using System;

    public delegate object SingleInstanceFactory(Type serviceType);
}