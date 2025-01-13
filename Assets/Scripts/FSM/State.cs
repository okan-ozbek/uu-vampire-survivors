using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace FSM
{
    public abstract class State
    {
        protected  bool IsRootState { get; set; }
        
        public State ParentState { get; protected set; }
        public State SubState { get; protected set; }
        
        private HashSet<Transition> Transitions { get; }
        private HashSet<Transition> SubTransitions { get; }

        private StateMachine StateMachine { get; }

        protected State(StateMachine stateMachine)
        {
            Transitions = new HashSet<Transition>();
            SubTransitions = new HashSet<Transition>();
            StateMachine = stateMachine;
            
            Initialize();
        }

        public void Enter()
        {
            TransitionSubState();
            
            OnEnter();
            SubState?.Enter();
        }
        
        public void Exit()
        {
            SubState?.Exit();
            OnExit();
        }
        
        public void Update()
        {
            OnUpdate();
            SubState?.Update();

            Transition();
            TransitionSubState();
        }
        
        protected virtual void OnEnter()
        {
        }

        protected virtual void OnExit()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        protected abstract void SetTransitions();
        
        protected void AddTransition(Type stateType, Func<bool> condition)
        {
            Transitions.Add(new Transition(stateType, condition));
        }

        protected void AddSubTransition(Type stateType, Func<bool> condition)
        {
            SubTransitions.Add(new Transition(stateType, condition));
        }

        private void Initialize()
        {
            SetTransitions();
        }

        private void Transition()
        {
            Transition transition = GetTransition(Transitions);
            
            if (transition == null)
            {
                return;
            }
            
            Exit();
            if (IsRootState)
            {
                StateMachine.State = StateMachine.Factory.GetState(transition.To);
                StateMachine.State.Enter();
            }
            else
            {
                if (ParentState != null)
                {
                    // TODO I think this needs an on enter and exit, not sure tho'
                    ParentState.SubState = StateMachine.Factory.GetState(transition.To);
                }
            }
        }

        private void TransitionSubState()
        {
            Transition transition = GetTransition(SubTransitions);
            
            if (transition == null)
            {
                return;
            }
            
            SubState?.Exit();
            SubState = StateMachine.Factory.GetState(transition.To);
            SubState.ParentState = this;
            SubState.Enter();
        }
        
        [CanBeNull]
        private Transition GetTransition(HashSet<Transition> transitions)
        {
            if (transitions.Count == 0)
            {
                return null;
            }
            
            foreach (var transition in transitions)
            {
                if (transition.Condition() && transition.To != typeof(State))
                {
                    return transition;
                }
            }

            return null;
        }
    }
}