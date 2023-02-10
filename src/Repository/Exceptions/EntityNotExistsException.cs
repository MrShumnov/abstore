using System;
using Repository.Entity;

namespace Repository.Exceptions
{
    public class EntityNotExistsException : RepositoryException
    {
        public EntityNotExistsException(string message) : base(message) { }
    }
}
