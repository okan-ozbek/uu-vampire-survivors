using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace HFSM
{
    public interface IState
    {
        public bool IsRootState { get; }
        
        public IState ParentState { get; set; }
        public IState ChildState { get; set; }
        
        public HashSet<ITransition> Transitions { get; }
        public HashSet<ITransition> ChildTransitions { get; }
        
        void Enter();
        void Exit();
        void Update();
    }
    
    public abstract class State : IState
    {
        public bool IsRootState { get; private set; } = false;
        
        public IState ParentState { get; set; }
        public IState ChildState { get; set; }
        
        public HashSet<ITransition> Transitions { get; private set; }
        public HashSet<ITransition> ChildTransitions { get; private set; }

        protected State()
        {
            GetState();
        }

        public void Enter()
        {
            OnEnter();
            ChildState?.Enter();
        }

        public void Exit()
        {
            ChildState?.Exit();
            OnExit();
        }

        public void Update()
        {
            OnUpdate();
            ChildState?.Update();
        }

        protected abstract void OnEnter();
        protected abstract void OnExit();
        protected abstract void OnUpdate();
        
        protected abstract void SetTransitions();
        protected abstract void SetChildTransitions();
        
        protected void AddTransition(Type toStateType, Func<bool> condition)
        {
            Transitions.Add(new Transition(toStateType, condition));
        }
        
        protected void AddChildTransition(Type toStateType, Func<bool> condition)
        {
            ChildTransitions.Add(new Transition(toStateType, condition));
        }

        protected void EnableRootState()
        {
            IsRootState = true;
        }

        private void GetState()
        {
            Transitions = new HashSet<ITransition>();
            ChildTransitions = new HashSet<ITransition>();
            
            SetTransitions();
            SetChildTransitions();
        }
    }
}