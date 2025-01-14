using Entities.Player.States;
using HFSM;
using Serializables;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerCore : MonoBehaviour
    {
        [SerializeField] private GameObject swordHitbox;
        [SerializeField] private PlayerData data;
        
        public GameObject SwordHitbox => swordHitbox;
        public PlayerData Data => data;
        
        public Rigidbody2D Body { get; set; }
        public IStateFactory StateFactory { get; set; }
        public IStateMachine StateMachine { get; set; }
        
        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            StateFactory = new PlayerStateFactory(this);
            
            StateMachine = new StateMachine(StateFactory, typeof(PlayerLocomotion));
            StateMachine.CurrentState.Enter();
        }
        
        private void Update()
        {
            StateMachine.Transition(StateMachine.CurrentState);
            StateMachine.CurrentState.Update();
            
            Debug.Log(StateMachine.GetTree(StateMachine.CurrentState));
        }
    }
}