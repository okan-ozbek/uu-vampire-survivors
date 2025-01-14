using System.Collections;
using Configs;
using Controllers;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerBasicAttack : PlayerState
    {
        private readonly TimeController _timeController;
        
        public PlayerBasicAttack(PlayerCore core) : base(core)
        {
            EnableRootState();
            
            _timeController = new TimeController(0.2f);
        }

        protected override void OnEnter()
        {
            PlayerEventConfig.OnPlayerBasicAttack.Invoke(Core.Data.Guid);
            
            Core.SwordHitbox.transform.localScale = Vector3.one;
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

        protected override void SetChildTransitions()
        {
        }
    }
}