using Animations;
using Configs;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class EnemyHurt : EnemyState
    {
        private bool Completed { get; set; }
        
        public EnemyHurt(EnemyCore core) : base(core)
        {
            EnableRootState();
        }

        protected override void OnEnter()
        {
            SimpleAnimationStateBehaviour.OnAnimationCompleted += HandleOnAnimationCompleted;
            
            EnemyEventConfig.OnEnemyHurt?.Invoke(Core.Data.Guid);
            Core.Animator.PlayAnimation(EnemyAnimationType.Hurt);
            Core.Body.linearVelocity = Vector2.zero;

            Core.Data.health -= 10f;
        }

        protected override void OnExit()
        {
            SimpleAnimationStateBehaviour.OnAnimationCompleted -= HandleOnAnimationCompleted;

            Completed = false;
            Core.Data.isHurt = false;
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(EnemyIdle), () => Completed);
            AddTransition(typeof(EnemyDeath), () => Core.Data.health <= 0f);
        }
        
        private void HandleOnAnimationCompleted(int shortNameHash)
        {
            Completed = true;
        }
    }
}