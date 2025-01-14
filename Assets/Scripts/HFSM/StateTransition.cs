#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace HFSM
{
    public interface IStateTransition
    {
        public IStateFactory StateFactory { get; }
        public IStateMachine StateMachine { get; }
        
        public void Handle(IState state);
    }
    
    public class StateTransition : IStateTransition
    {
        public IStateFactory StateFactory { get; }
        public IStateMachine StateMachine { get; }

        public StateTransition(IStateFactory stateFactory, IStateMachine stateMachine)
        {
            StateFactory = stateFactory;
            StateMachine = stateMachine;
        }
        
        public void Handle(IState state)
        {
            Transition(state.Transitions, state, state.IsRootState);
            Transition(state.ChildTransitions, state);

            if (state.ChildState != null)
            {
                Handle(state.ChildState);
            }
        }

        private void Transition(HashSet<ITransition> transitions, IState state, bool isRootState = false)
        {
            ITransition? transition = GetTransition(transitions);

            if (transition == null)
            {
                return;
            }
            
            IState? newState = SetState(transition.ToStateType, state, isRootState);
            newState?.Enter();
        }

        private IState? SetState(Type toStateType, IState state, bool isRootState = false)
        {
            if (isRootState)
            {
                StateMachine.CurrentState.Exit();
                StateMachine.CurrentState = StateFactory.GetState(toStateType);
                return StateMachine.CurrentState;
            }

            if (state.ParentState != null)
            {
                state.ParentState.ChildState.Exit();
                state.ParentState.ChildState = StateFactory.GetState(toStateType);
                state.ParentState.ChildState.ParentState = state.ParentState;
                return state.ParentState.ChildState;
            }

            return null;
        }
        
        private ITransition? GetTransition(HashSet<ITransition> transitions)
        {
            if (transitions.Count == 0) 
            {
                return null;
            }

            foreach (ITransition transition in transitions.Where(transition => transition != null))
            {
                if (transition.Condition() && transition.ToStateType != typeof(State))
                {
                    return transition;
                }
            }

            return null;
        }
    }
}