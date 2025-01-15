﻿using System.Collections;
using Configs;
using Utility;
using UnityEngine;

namespace Entities.Player.States.Child
{
    public class PlayerDash : PlayerState
    {
        private bool _isFinished;
        
        public PlayerDash(PlayerCore core) : base(core)
        {
        }
        
        protected override void OnEnter()
        {
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
            AddTransition(typeof(PlayerIdle), () => _isFinished && Core.Body.linearVelocity.magnitude == 0 && PlayerInput.MovementDirection == Vector3.zero);
            AddTransition(typeof(PlayerMove), () => _isFinished && Core.Body.linearVelocity.magnitude > 0 && PlayerInput.MovementDirection != Vector3.zero);
        }
        
        private IEnumerator DashRoutine()
        {
            Core.Body.linearVelocity = PlayerInputController.MovementDirection.normalized * Core.Data.dashPower;
            
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