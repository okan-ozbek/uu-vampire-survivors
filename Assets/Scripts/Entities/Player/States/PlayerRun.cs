using System.Collections;
using Configs;
using UnityEngine;
using Utility;

namespace Entities.Player.States
{
    public class PlayerRun : PlayerState
    {
        private Coroutine FootstepCoroutine { get; set; }
        private const float FootstepInterval = 0.25f;
        
        public PlayerRun(PlayerCore core) : base(core)
        {
            EnableRootState();
        }

        protected override void OnEnter()
        {
            Core.Animator.PlayAnimation(PlayerAnimationType.Run);
            FootstepCoroutine = Core.StartCoroutine(TriggerFootstepSFX());   
        }
        
        protected override void OnUpdate()
        {
            Core.Movement.Accelerate(Core.Data.runSpeed);
            Core.Movement.Decelerate();
            Core.Movement.Brake();
        }
        
        protected override void OnExit()
        {
            Core.StopCoroutine(FootstepCoroutine);
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerIdle), () => PlayerInput.MovementDirection == Vector3.zero && Core.Body.linearVelocity == Vector2.zero);
            AddTransition(typeof(PlayerWalk), () => PlayerInput.RunKeyHeld == false);
            AddTransition(typeof(PlayerDash), () => PlayerInput.DashKeyPressed && Core.Data.canDash);
            AddTransition(typeof(PlayerHurt), () => PlayerInput.HurtKeyPressed);
            AddTransition(typeof(PlayerAttack), () => PlayerInput.AttackKeyPressed);
        }
        
        private IEnumerator TriggerFootstepSFX()
        {
            while (true)
            {
                yield return new WaitForSeconds(FootstepInterval);
                PlayerEventConfig.OnPlayerFootstep?.Invoke(Core.Data.Guid);
            }
        }
    }
}