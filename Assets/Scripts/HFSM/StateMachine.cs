#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HFSM
{
    public interface IStateMachine
    {
        public IStateFactory StateFactory { get; }
        public IState CurrentState { get; }

        public void Transition(IState state);
        public string GetTree(IState state);
    }

    public class StateMachine : IStateMachine
    {
        public IStateFactory StateFactory { get; private set; }
        public IState CurrentState { get; private set; }

        public StateMachine(IStateFactory stateFactory, Type initialState)
        {
            StateFactory = stateFactory;
            StateFactory.GetFactory();
            
            CurrentState = StateFactory.GetState(initialState);
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
        
        public void Transition(IState state)
        {
            HandleTransition(state.Transitions, state, state.IsRootState);
            HandleTransition(state.ChildTransitions, state);

            if (state.ChildState != null)
            {
                Transition(state.ChildState);
            }
        }

        private void HandleTransition(HashSet<ITransition> transitions, IState state, bool isRootState = false)
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
                CurrentState.Exit();
                CurrentState = StateFactory.GetState(toStateType);
                return CurrentState;
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