using Entities.Enemies.States;
using HFSM;
using UnityEngine;

namespace Entities.Enemies
{
    public class EnemyCore : Core
    {
        public Rigidbody2D Body { get; private set; }
        public EnemyAnimator Animator { get; private set; }
        
        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            Animator = new EnemyAnimator(GetComponentInChildren<Animator>());
            
            GetFactory(new EnemyStateFactory(this));
            GetStateMachine(typeof(EnemyIdle));
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
                UnityEditor.Handles.Label(transform.position, "Active: " + StateMachine.GetTree(StateMachine.CurrentState));
            }
            #endif
        }
    }
}