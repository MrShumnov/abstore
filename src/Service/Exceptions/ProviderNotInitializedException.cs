using System;
using Repository.Entity;

namespace Service.Exceptions
{
    public class ProviderNotInitializedException : ServiceException
    {
        public ProviderNotInitializedException(string message = "") 
            : base(message) { }
    }
}
