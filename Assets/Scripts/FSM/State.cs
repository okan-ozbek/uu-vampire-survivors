using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public abstract class State
    {
        public HashSet<Transition> Transitions { get; }

        protected State()
        {
            Transitions = new HashSet<Transition>();
            
            Initialize();
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }

        protected abstract void SetTransitions();
        
        protected void AddTransition(Type stateType, Func<bool> condition)
        {
            Transitions.Add(new Transition(stateType, condition));
        }

        private void Initialize()
        {
            SetTransitions();
        }
    }
}