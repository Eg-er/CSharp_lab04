using System;
namespace Lab02.Exceptions
{
    internal class BadSurnameException : Exception
    {
        public BadSurnameException()
        {
        }

        public BadSurnameException(string message)
            : base(message)
        {
        }

        public BadSurnameException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}