using System;
namespace Lab02.Exceptions
{
    internal class FutureBirthDateException : Exception
    {
        public FutureBirthDateException()
        {
        }

        public FutureBirthDateException(string message)
            : base(message)
        {
        }

        public FutureBirthDateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}