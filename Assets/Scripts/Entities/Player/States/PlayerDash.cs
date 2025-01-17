using System.Collections;
using Configs;
using UnityEngine;
using Utility;

namespace Entities.Player.States
{
    public class PlayerDash : PlayerState
    {
        private bool _isFinished;
        
        public PlayerDash(PlayerCore core) : base(core)
        {
            EnableRootState();
        }
        
        protected override void OnEnter()
        {
            Core.Animator.PlayAnimation(PlayerAnimationType.Dash);
            PlayerEventConfig.OnPlayerDash.Invoke(Core.Data.Guid);
            
            Core.StartCoroutine(DashRoutine());
            Core.StartCoroutine(DashCooldown());
        }

        protected override void OnExit()
        {
            _isFinished = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerIdle), () => _isFinished && PlayerInput.MovementDirection == Vector3.zero && Core.Body.linearVelocity == Vector2.zero);
            AddTransition(typeof(PlayerRun), () => _isFinished && PlayerInput.MovementDirection != Vector3.zero);
            AddTransition(typeof(PlayerHurt), () => PlayerInput.HurtKeyPressed);
        }
        
        private IEnumerator DashRoutine()
        {
            Core.Body.linearVelocity = PlayerInput.MovementDirection.normalized * Core.Data.dashPower;
            
            yield return new WaitForSeconds(Core.Data.dashDuration);
            
            _isFinished = true;
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