namespace AppStrap.Logging
{
    using System;

    public interface IRegistrar
    {
        void Register(Guid id, string text);
    }
}