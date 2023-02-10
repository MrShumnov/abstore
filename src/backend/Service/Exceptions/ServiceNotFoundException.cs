using System;
using Repository.Entity;

namespace Service.Exceptions
{
    public class ServiceNotFoundException : ServiceException
    {
        public ServiceNotFoundException(string message = "") : base(message) { }
    }
}
