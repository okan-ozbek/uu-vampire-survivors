using System;
using UnityEngine;

namespace FSM
{
    public abstract class StateMachine : MonoBehaviour
    {
        public State State { get; set; }
        private StateFactory Factory { get; set; }

        protected virtual void Update()
        {
            State.Update();

            Debug.Log(GetTree(State));
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

        // Recursively get all the substates
        private string GetTree(State state)
        {
            string tree = state.GetType().Name;

            if (state.SubState != null)
            {
                tree += " -> " + GetTree(state.SubState);
            }

            return tree;
        }
    }
}