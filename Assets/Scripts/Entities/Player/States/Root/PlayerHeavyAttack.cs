using Configs;
using Entities.Player.States.Child;
using UnityEngine;
using Utility;

namespace Entities.Player.States.Root
{
    public class PlayerHeavyAttack : PlayerState
    {
        private Timer Timer { get; }
        
        public PlayerHeavyAttack(PlayerCore core) : base(core)
        {
            EnableRootState();
            
            Timer = new Timer(0.2f);
        }

        protected override void OnEnter()
        {
            SetChild(typeof(PlayerDecelerate));
            
            PlayerEventConfig.OnPlayerHeavyAttack.Invoke(Core.Data.Guid);
            
            Core.SwordHitbox.transform.localScale = Vector3.one * 2f;
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