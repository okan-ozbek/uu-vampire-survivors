using Configs;
using Controllers;
using Controllers.Player;
using Entities.Player.States.Child;
using UnityEngine;

namespace Entities.Player.States.Root
{
    public class PlayerChargeAttack : PlayerState
    {   
        private Timer Timer { get; }
        private bool CanTransition { get; set; }
        
        public PlayerChargeAttack(PlayerCore core) : base(core)
        {
            EnableRootState();
            
            Timer = new Timer(1f);
        }
        
        protected override void OnEnter()
        {
            SetChild(typeof(PlayerChargeIdle));
        }
        
        protected override void OnExit()
        {
            Timer.Reset();

            CanTransition = false;
        }
        
        protected override void OnUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                CanTransition = true;
            }
            
            Timer.Update();
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