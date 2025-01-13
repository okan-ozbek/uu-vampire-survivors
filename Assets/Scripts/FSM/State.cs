using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace FSM
{
    public abstract class State
    {
        public bool IsRootState { get; set; } = false;
        public State ParentState { get; set; }
        public State SubState { get; set; }
        
        public HashSet<Transition> Transitions { get; }
        public HashSet<Transition> SubTransitions { get; }
        
        public StateMachine StateMachine { get; }
        public StateFactory Factory { get; set; }

        protected State(StateMachine stateMachine, StateFactory factory)
        {
            Transitions = new HashSet<Transition>();
            SubTransitions = new HashSet<Transition>();
            StateMachine = stateMachine;
            Factory = factory;
            
            Initialize();
        }

        public virtual void Enter()
        {
            SubState?.Enter();
        }

        public virtual void Exit()
        {
            SubState?.Exit();
        }

        public virtual void Update()
        {
            SubState?.Update();
            
            Transition();
            TransitionSubState();
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
            
            if (transition == null || transition.To == typeof(State))
            {
                return;
            }

            Exit();
            if (IsRootState)
            {
                StateMachine.State = Factory.GetState(transition.To);
                StateMachine.State.Enter();
            }
            else
            {
                if (ParentState != null)
                {
                    ParentState.SubState = Factory.GetState(transition.To);
                    ParentState.SubState.Enter();    
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
            SubState = Factory.GetState(transition.To);
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