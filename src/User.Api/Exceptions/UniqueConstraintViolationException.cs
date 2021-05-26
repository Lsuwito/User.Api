using System;

namespace User.Api.Exceptions
{
    /// <summary>
    /// Wrapper for database specific unique constraint violation exception.
    /// We want to handle unique constraint violation so we can return conflict status code,
    /// but we do not want to reference any database specific exception in our domain code.
    /// In case if we switch database flavor, we dont have to change the error handling code.
    /// </summary>
    public class UniqueConstraintViolationException : Exception
    {
        public UniqueConstraintViolationException(Exception innerException) : base("Duplicate key value violation.", innerException) 
        {
        }
    }
}