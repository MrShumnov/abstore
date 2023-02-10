using System;
using Repository.Entity;

namespace Repository.Exceptions
{
    public class EntityDuplicateException : RepositoryException
    {
        public EntityDuplicateException(string message) 
            : base(message) { }
    }
}
