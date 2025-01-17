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
            SimpleAnimationStateBehaviour.OnAnimationCompleted += HandleOnAnimationCompleted;
            SimpleAnimationStateBehaviour.OnAnimationTriggerActivated += HandleOnAnimationTriggerActivated;
            
            PlayerEventConfig.OnPlayerAttack?.Invoke(Core.Data.Guid);
            Core.Animator.PlayAnimation(PlayerAnimationType.Attack);
            Core.Body.linearVelocity = Vector2.zero;
        }

        protected override void OnExit()
        {
            SimpleAnimationStateBehaviour.OnAnimationCompleted -= HandleOnAnimationCompleted;
            SimpleAnimationStateBehaviour.OnAnimationTriggerActivated -= HandleOnAnimationTriggerActivated;

            Completed = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerIdle), () => Completed);
        }
        
        private void HandleOnAnimationCompleted(string animationName)
        {
            Completed = true;
        }

        private void HandleOnAnimationTriggerActivated(string triggerName)
        {
            PlayerEventConfig.OnPlayerAttackEnd?.Invoke(Core.Data.Guid);
        }
    }
}