#nullable enable
using System;

namespace HFSM
{
    public interface IStateMachine
    {
        public IStateFactory StateFactory { get; }
        public IState CurrentState { get; set; }
        public IStateTransition StateTransition { get; }

        public string GetTree(IState state);
    }

    public class StateMachine : IStateMachine
    {
        public IStateFactory StateFactory { get; }
        public IState CurrentState { get; set; }
        public IStateTransition StateTransition { get; }

        public StateMachine(IStateFactory stateFactory, Type initialState)
        {
            StateFactory = stateFactory;
            StateFactory.GetFactory();
            
            CurrentState = StateFactory.GetState(initialState);
            CurrentState.Enter();
            
            StateTransition = new StateTransition(StateFactory, this);
        }
        
        public string GetTree(IState state)
        {
            string tree = state.GetType().Name;

            if (state.ChildState != null)
            {
                tree += " -> " + GetTree(state.ChildState);
            }

            return tree;
        }
    }
}