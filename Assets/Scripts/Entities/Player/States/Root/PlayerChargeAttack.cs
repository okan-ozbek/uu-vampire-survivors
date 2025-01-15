using Configs;
using Controllers;
using Controllers.Player;
using Entities.Player.States.Child;
using UnityEngine;

namespace Entities.Player.States.Root
{
    public class PlayerChargeAttack : PlayerState
    {
        private const float ReduceMaxSpeedBufferTime = 0.15f;
        
        private Timer Timer { get; }
        private bool CanTransition { get; set; }

        private float _maxSpeed;
        
        public PlayerChargeAttack(PlayerCore core) : base(core)
        {
            EnableRootState();
            
            _timeController = new TimeController(1f);
        }
        
        protected override void OnEnter()
        {
            SetChild(typeof(PlayerIdle));
            
            _maxSpeed = Core.Data.maxSpeed;
        }
        
        protected override void OnExit()
        {
            Timer.Reset();

            CanTransition = false;
            Core.Data.maxSpeed = _maxSpeed;
        }
        
        protected override void OnUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                CanTransition = true;
            }
            
            Timer.Update();
            if (Timer.TimePassed > ReduceMaxSpeedBufferTime)
            {
                Core.Data.maxSpeed = 1.5f;
            }

            if (Timer.Completed) 
            {
                PlayerEventConfig.OnPlayerChargeAttack.Invoke(Core.Data.Guid, 1f);
            }
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerLocomotion), () => Input.GetKeyDown(KeyCode.Mouse1));
            AddTransition(typeof(PlayerBasicAttack), () => CanTransition && Timer.Completed == false);
            AddTransition(typeof(PlayerHeavyAttack), () => CanTransition && Timer.Completed);
        }
    }
}