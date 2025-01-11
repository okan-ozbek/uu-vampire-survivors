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
        }
        
        public override void Enter()
        {
            PlayerEventConfig.OnPlayerDash?.Invoke(Core.Data.Guid);
            Core.StartCoroutine(DashRoutine());
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(Run), () => _stateFinished);
        }

        private IEnumerator DashRoutine()
        {
            _stateFinished = false;
            Core.Body.linearVelocity = PlayerInputController.NormalizedMovementDirection * Core.Data.dashPower;
            
            yield return new WaitForSeconds(Core.Data.dashDuration);
            
            _stateFinished = true;
        }
    }
}