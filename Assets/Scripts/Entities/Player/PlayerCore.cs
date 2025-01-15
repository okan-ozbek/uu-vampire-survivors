using System;
using Entities.Player.States.Root;
using HFSM;
using Serializables;
using UnityEngine;

namespace Entities.Player
{
    
    public class PlayerCore : Core
    {
        [SerializeField] private GameObject swordHitbox; // TODO: This needs to be better
        [SerializeField] private PlayerData data;
        
        public GameObject SwordHitbox => swordHitbox; // TODO: This needs to be better
        public PlayerData Data => data;
        
        public Rigidbody2D Body { get; private set; }
        
        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            
            GetFactory(new PlayerStateFactory(this));
            GetStateMachine(typeof(PlayerLocomotion));
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