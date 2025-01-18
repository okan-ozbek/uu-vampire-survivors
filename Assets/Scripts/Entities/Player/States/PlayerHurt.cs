using Animations;
using Configs;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerHurt : PlayerState
    {
        private bool Completed { get; set; }
        
        public PlayerHurt(PlayerCore core) : base(core)
        {
            EnableRootState();
        }

        protected override void OnEnter()
        {
            SimpleAnimationStateBehaviour.OnAnimationCompleted += HandleOnAnimationCompleted;
            
            PlayerEventConfig.OnPlayerHurt?.Invoke(Core.Data.Guid);
            Core.Animator.PlayAnimation(PlayerAnimationType.Hurt);
            Core.Body.linearVelocity = Vector2.zero;
        }

        protected override void OnExit()
        {
            SimpleAnimationStateBehaviour.OnAnimationCompleted -= HandleOnAnimationCompleted;

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
    }
}