using System;

namespace User.Api.Exceptions
{
    /// <summary>
    /// Wrapper for database specific unique constraint violation exception.
    ///
    /// Purpose: Database specific exception should not bubble up to the domain code.
    /// In case if we switch database flavor, we dont have to change the error handling code.
    /// </summary>
    public class UniqueConstraintViolationException : Exception
    {
        public UniqueConstraintViolationException(Exception innerException) : base("Duplicate key value violation.", innerException) 
        {
        }
    }
}