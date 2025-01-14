using Entities.Player.States;
using HFSM;
using Serializables;
using UnityEngine;

namespace Entities.Player
{
    
    public class PlayerCore : Core
    {
        [SerializeField] private GameObject swordHitbox;
        [SerializeField] private PlayerData data;
        
        public GameObject SwordHitbox => swordHitbox;
        public PlayerData Data => data;
        
        public Rigidbody2D Body { get; set; }

        
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
            
            Debug.Log(StateMachine.GetTree(StateMachine.CurrentState));
        }
    }
}