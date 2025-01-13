using System.Collections;
using Configs;
using Controllers.Player;
using UnityEngine;

namespace Entities.Player.States
{
    public class Dash : PlayerState
    {
        private bool _stateFinished;
        
        public Dash(PlayerCore core) : base(core)
        {
            IsRootState = true;
        }

        protected override void OnEnter()
        {
            PlayerEventConfig.OnPlayerDash?.Invoke(Core.Data.Guid);
            Core.StartCoroutine(DashRoutine());
            Core.StartCoroutine(DashCooldown());
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(Run), () => _stateFinished);
        }

        private IEnumerator DashRoutine()
        {
            _stateFinished = false;
            Core.Body.linearVelocity = PlayerInputController.MovementDirection * Core.Data.dashPower;
            
            yield return new WaitForSeconds(Core.Data.dashDuration);
            
            _stateFinished = true;
        }
        
        private IEnumerator DashCooldown()
        {
            Core.Data.canDash = false;
            PlayerEventConfig.OnDashCooldown?.Invoke(Core.Data.Guid, Core.Data.dashCooldown);
            
            yield return new WaitForSeconds(Core.Data.dashCooldown);
            
            Core.Data.canDash = true;
        }
    }
}