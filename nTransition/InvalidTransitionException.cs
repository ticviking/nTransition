using System;

namespace nTransition
{
    public class InvalidTransitionException : Exception
    {
        public InvalidTransitionException(string s) : base(s)
        {
        }
    }
}
