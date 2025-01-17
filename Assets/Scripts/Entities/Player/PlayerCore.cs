using Entities.Player.States;
using HFSM;
using Serializables;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerCore : Core
    {
        [SerializeField] private PlayerData data;
        
        public PlayerData Data => data;
        
        public Rigidbody2D Body { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public PlayerAnimator Animator { get; private set; }
        
        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            Animator = new PlayerAnimator(GetComponentInChildren<Animator>());
            Movement = new PlayerMovement(this);
            
            GetFactory(new PlayerStateFactory(this));
            GetStateMachine(typeof(PlayerIdle));
        }
        
        private void Update()
        {
            StateMachine.StateTransition.Handle(StateMachine.CurrentState);
            StateMachine.CurrentState.Update();

            Movement.Flip();
        }

        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
            if (Application.isPlaying)
            {
                UnityEditor.Handles.Label(transform.position, "Active: " + StateMachine.GetTree(StateMachine.CurrentState));
            }
            #endif
        }
    }
}