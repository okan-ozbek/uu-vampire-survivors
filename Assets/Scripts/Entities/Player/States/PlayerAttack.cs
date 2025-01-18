using Animations;
using Configs;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerAttack : PlayerState
    {
        private bool Completed { get; set; }
        
        public PlayerAttack(PlayerCore core) : base(core)
        {
            EnableRootState();
        }

        protected override void OnEnter()
        {
            Core.AttackHitbox.SetActive(true);
            Core.Body.WakeUp();
            
            SimpleAnimationStateBehaviour.OnAnimationCompleted += HandleOnAnimationCompleted;
            SimpleAnimationStateBehaviour.OnAnimationTriggerActivated += HandleOnAnimationTriggerActivated;
            
            PlayerEventConfig.OnPlayerAttack?.Invoke(Core.Data.Guid);
            Core.Animator.PlayAnimation(PlayerAnimationType.Attack);
            Core.Body.linearVelocity = Vector2.zero;
        }

        protected override void OnExit()
        {
            Core.AttackHitbox.SetActive(false);
            
            SimpleAnimationStateBehaviour.OnAnimationCompleted -= HandleOnAnimationCompleted;
            SimpleAnimationStateBehaviour.OnAnimationTriggerActivated -= HandleOnAnimationTriggerActivated;

            Completed = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerIdle), () => Completed);
        }
        
        private void HandleOnAnimationCompleted(int shortNameHash)
        {
            Completed = true;
        }

        private void HandleOnAnimationTriggerActivated(int shortNameHash)
        {
            PlayerEventConfig.OnPlayerAttackEnd?.Invoke(Core.Data.Guid);
        }
    }
}