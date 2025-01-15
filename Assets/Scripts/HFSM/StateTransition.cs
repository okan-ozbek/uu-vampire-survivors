#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

            if (state?.ChildState != null)
            {
                Handle(state.ChildState);
            }
        }

        private void Transition(HashSet<ITransition> transitions, IState? state, bool isRootState = false)
        {
            ITransition? transition = GetTransition(transitions);

            if (transition == null)
            {
                return;
            }
            
            SetState(transition.ToStateType, state, isRootState);
        }

        private void SetState(Type toStateType, IState? state, bool isRootState = false)
        {
            if (isRootState)
            {
                if (StateMachine.CurrentState.GetType() == toStateType)
                {
                    return;
                }
                
                StateMachine.CurrentState.Exit();
                StateMachine.CurrentState = StateFactory.GetState(toStateType);
                StateMachine.CurrentState.Enter();
                
                return;
            }
            
            if (state?.ParentState != null)
            {
                if (state.ParentState.ChildState != null && state.ParentState.ChildState.GetType() == toStateType)
                {
                    return;
                }
            
                state.ParentState.ChildState?.Exit();
                state.ParentState.ChildState = StateFactory.GetState(toStateType);
                state.ParentState.ChildState.ParentState = state.ParentState;
                state.ParentState.ChildState.Enter();
            }
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