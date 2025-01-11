using System;
using JetBrains.Annotations;
using UnityEngine;

namespace FSM
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State State { get; set; }
        private StateFactory Factory { get; set; }

        protected virtual void Update()
        {
            State.Update();
            Transition();
        }

        protected void InitializeFactory(StateFactory factory)
        {
            Factory = factory;
        }
        
        protected void InitializeState(Type stateType)
        {
            State = Factory.GetState(stateType);
            State.Enter();
        }
        
        private void Transition()
        {
            Transition transition = GetTransition();

            if (transition == null)
            {
                return;
            }
            
            State?.Exit();
            State = Factory.GetState(transition.To);
            State.Enter();
        }

        [CanBeNull]
        private Transition GetTransition()
        {
            foreach (var transition in State.Transitions)
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