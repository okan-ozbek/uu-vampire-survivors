using System;
using UnityEngine;

namespace HFSM
{
    public interface ICore
    {
        public IStateFactory StateFactory { get; set; }
        public IStateMachine StateMachine { get; set; }
    }
    
    public class Core : MonoBehaviour, ICore
    {
        public IStateFactory StateFactory { get; set; }
        public IStateMachine StateMachine { get; set; }

        protected void GetFactory(IStateFactory stateFactory)
        {
            StateFactory = stateFactory;
            StateFactory.GetFactory();
        }

        protected void GetStateMachine(Type initialStateType)
        {
            StateMachine = new StateMachine(StateFactory, initialStateType);
        }
    }
}