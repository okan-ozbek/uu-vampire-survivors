using Entities.Enemies.States;
using HFSM;
using Serializables;
using Unity;
using UnityEngine;

namespace Entities.Enemies
{
    public class EnemyCore : Core
    {
        [SerializeField] private EnemyData data;
        [SerializeField] private GameObject art;
        
        public EnemyData Data => data;
        public GameObject Art => art;
        
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(UnityTag.PlayerAttackHitbox.ToString()))
            {
                Data.isHurt = true;
            }
        }
        
        public void Death()
        {
            Destroy(gameObject);
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