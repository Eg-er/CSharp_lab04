using System;
namespace Lab02.Exceptions
{
   internal class InvalidEmailException : Exception
    {
        public InvalidEmailException()
        {
        }

        public InvalidEmailException(string message)
            : base(message)
        {
        }

        public InvalidEmailException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}