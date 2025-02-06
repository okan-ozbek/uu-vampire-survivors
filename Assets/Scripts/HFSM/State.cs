using System;
using System.Collections.Generic;

namespace HFSM
{
    public interface IState
    {
        public bool IsRootState { get; }
        
        public IState ParentState { get; set; }
        public IState ChildState { get; set; }
        
        public HashSet<ITransition> Transitions { get; }
        
        public ICore Core { get; }
        
        void Enter();
        void Exit();
        void Update();
    }
    
    public abstract class State : IState
    {
        public bool IsRootState { get; private set; }
        
        public IState ParentState { get; set; }
        public IState ChildState { get; set; }
        
        public HashSet<ITransition> Transitions { get; private set; }

        public ICore Core { get; }

        protected State(ICore core)
        {
            Core = core;
            SetupState();
        }
        
        public void Enter()
        {
            OnEnter();
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

        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected virtual void OnUpdate() { }
        
        protected abstract void SetTransitions();
        
        protected void AddTransition(Type toStateType, Func<bool> condition)
        {
            Transitions.Add(new Transition(toStateType, condition));
        }

        protected void EnableRootState()
        {
            IsRootState = true;
        }

        protected void SetChild(Type childStateType)
        {
            ChildState = Core.StateFactory.GetState(childStateType);
            ChildState.ParentState = this;
            ChildState.Enter();
        }
        
        private void SetupState()
        {
            Transitions = new HashSet<ITransition>();
            SetTransitions();
        }
    }
}