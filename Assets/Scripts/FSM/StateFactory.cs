using System;
using System.Collections.Generic;

namespace FSM
{
    public abstract class StateFactory
    {
        private readonly Dictionary<Type, State> _states = new();

        public State GetState(Type stateType)
        {
            return _states.GetValueOrDefault(stateType);
        }
        
        protected abstract void SetStates();
        
        protected void AddState(State state)
        {
            _states.Add(state.GetType(), state);
        }
    }
}