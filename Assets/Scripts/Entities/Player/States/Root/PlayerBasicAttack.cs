using Configs;
using Utility;
using Controllers.Player;
using Entities.Player.States.Child;
using UnityEngine;

namespace Entities.Player.States.Root
{
    public class PlayerBasicAttack : PlayerState
    {
        private Timer Timer { get; }
        
        public PlayerBasicAttack(PlayerCore core) : base(core)
        {
            EnableRootState();
            
            Timer = new Timer(0.2f);
        }

        protected override void OnEnter()
        {
            SetChild(typeof(PlayerDecelerate));
            
            PlayerEventConfig.OnPlayerBasicAttack.Invoke(Core.Data.Guid);
            
            Core.SwordHitbox.transform.localScale = Vector3.one;
            Core.SwordHitbox.SetActive(true);
        }

        protected override void OnExit()
        {   
            Timer.Reset();
            
            Core.SwordHitbox.SetActive(false);
        }

        protected override void OnUpdate()
        {
            Timer.Update();
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerLocomotion), () => Timer.Completed);
        }
    }
}