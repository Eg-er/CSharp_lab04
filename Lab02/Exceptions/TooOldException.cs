using System;
namespace Lab02.Exceptions
{
    internal class TooOldException : Exception
    {
        public TooOldException()
        {
        }

        public TooOldException(string message)
            : base(message)
        {
        }

        public TooOldException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}