using System;

namespace Service.Exceptions
{
    public abstract class ServiceException : Exception
    {
        public ServiceException(string message) : base(message) { }
    }
}
