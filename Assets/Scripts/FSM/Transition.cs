using System;

namespace FSM
{
    public class Transition
    {
        public Type To { get; }
        public Func<bool> Condition { get; }
        
        public Transition(Type to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }
}