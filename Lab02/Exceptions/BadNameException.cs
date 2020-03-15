using System;
namespace Lab02.Exceptions
{
    internal class BadNameException : Exception
    {
        public BadNameException()
        {
        }

        public BadNameException(string message)
            : base(message)
        {
        }

        public BadNameException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}