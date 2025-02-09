
using Configs;
using Entities.Player.Factories;
using Entities.Player.States;
using HFSM;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerCore : Core
    {
        private void Awake()
        {
            GetFactory(new PlayerStateFactory(this));
            GetStateMachine(typeof(PlayerIdle));
        }

        private void Update()
        {
            StateMachine.StateTransition.Handle(StateMachine.CurrentState);
            StateMachine.CurrentState.Update();
        }
        
        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
            if (Application.isPlaying)
            {
                // Get all the active states as a tree
                UnityEditor.Handles.Label(transform.position, "Active: " + StateMachine.GetTree(StateMachine.CurrentState));
            }
            #endif
        }
    }
}