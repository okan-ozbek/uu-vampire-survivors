using System.Collections;
using Configs;
using Controllers.Player;
using UnityEngine;

namespace Entities.Player.States
{
    public class Dash : PlayerState
    {
        private bool _stateFinished;
        
        public Dash(PlayerCore core, PlayerFactory factory) : base(core, factory)
        {
            IsRootState = true;
        }
        
        public override void Enter()
        {
            
            
            PlayerEventConfig.OnPlayerDash?.Invoke(Core.Data.Guid);
            Debug.Log("current velocity " + Core.Body.linearVelocity);
            Vector3 dashVelocity = PlayerInputController.MovementDirection * Core.Data.dashPower;
            Core.Body.linearVelocity = dashVelocity;
            
            Core.StartCoroutine(DashRoutine(dashVelocity));
            // Core.StartCoroutine(DashCooldown());
            
            base.Enter();
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(Run), () => _stateFinished);
        }

        private IEnumerator DashRoutine(Vector3 dashVelocity)
        {
            _stateFinished = false;
            Debug.Log(Core.Body.linearVelocity);
            Core.Body.linearVelocity = dashVelocity;
            Debug.Log(Core.Body.linearVelocity);
            
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