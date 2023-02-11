using System;
using Repository.Entity;

namespace Service.Exceptions
{
    public class InvalidDtoException : ServiceException
    {
        public InvalidDtoException(string message = "") : base(message) { }
    }
}
