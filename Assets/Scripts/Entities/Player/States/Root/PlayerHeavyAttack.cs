using Configs;
using Controllers;
using Controllers.Player;
using Entities.Player.States.Child;
using UnityEngine;

namespace Entities.Player.States.Root
{
    public class PlayerHeavyAttack : PlayerState
    {
        private readonly TimeController _timeController;
        
        public PlayerHeavyAttack(PlayerCore core) : base(core)
        {
            EnableRootState();
            
            _timeController = new TimeController(0.2f);
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
            _timeController.Reset();
            
            Core.SwordHitbox.SetActive(false);
        }

        protected override void OnUpdate()
        {
            _timeController.Update();
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerLocomotion), () => _timeController.IsFinished());
        }
        
    }
}