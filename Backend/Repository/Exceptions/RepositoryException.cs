using System;

namespace Repository.Exceptions
{
    public abstract class RepositoryException : Exception
    {
        public RepositoryException(string message) : base(message) { }
    }
}
