using System;

namespace eStateMachine
{
    public class InvalidTransitionException : Exception
    {
        public InvalidTransitionException(string s) : base(s)
        {
        }
    }
}