using System;
using Repository.Entity;

namespace Service.Exceptions
{
    public class ObjectNotFoundException : ServiceException
    {
        public ObjectNotFoundException(string message = "") : base(message) { }
    }
}
