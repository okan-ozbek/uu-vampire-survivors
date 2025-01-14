using System;

namespace HFSM
{
    public interface ITransition
    {
        Type ToStateType { get; }
        Func<bool> Condition { get; }
    }
    
    public class Transition : ITransition
    {
        public Type ToStateType { get; private set; }
        public Func<bool> Condition { get; private set; }
        
        public Transition(Type toStateType, Func<bool> condition)
        {
            ToStateType = toStateType;
            Condition = condition;
        }
    }
}