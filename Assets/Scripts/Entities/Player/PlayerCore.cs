using Controllers.Player;
using Entities.Player.States;
using FSM;
using Serializables;
using TMPro;
using UnityEngine;

namespace Entities.Player
{
    public sealed class PlayerCore : StateMachine
    {
        [SerializeField] private PlayerData data;
        [SerializeField] private GameObject swordHitbox;

        // TODO: Remove later
        public TMP_Text Text { get; private set; }
        public GameObject SwordHitbox => swordHitbox;
        
        public PlayerData Data => data;
        public Rigidbody2D Body { get; private set; }
        
        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            
            // TODO: Remove later
            Text = GetComponentInChildren<TMP_Text>();
            
            InitializeFactory(new PlayerFactory(this));
            InitializeState(typeof(Idle));
        }

        protected override void Update()
        {
            base.Update();
            
            PlayerInputController.Update();
            ReverseSprite();
        }

        private void ReverseSprite()
        {
            Vector3 localScale = transform.localScale;

            localScale.x = Body.linearVelocity.x switch
            {
                > 0.0f => Mathf.Abs(localScale.x),
                < 0.0f => -Mathf.Abs(localScale.x),
                _ => localScale.x
            };

            transform.localScale = localScale;
        }
    }
}