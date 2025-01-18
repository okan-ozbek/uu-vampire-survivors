using Animations;
using Configs;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class EnemyDeath : EnemyState
    {
        private static readonly Vector3 DeathAnimationOffset = new(-1.35f, 0.85f, 0f);

        public EnemyDeath(EnemyCore core) : base(core)
        {
            EnableRootState();
        }

        protected override void OnEnter()
        {
            SimpleAnimationStateBehaviour.OnAnimationCompleted += HandleOnAnimationCompleted;
            
            EnemyEventConfig.OnEnemyDeath?.Invoke(Core.Data.Guid);
            Core.Animator.PlayAnimation(EnemyAnimationType.Death);
            
            Core.Art.transform.localPosition = DeathAnimationOffset;
            Core.Body.linearVelocity = Vector2.zero;
        }

        protected override void SetTransitions()
        {
        }

        private void HandleOnAnimationCompleted(int shortNameHash)
        {
            if (Animator.StringToHash(EnemyAnimationType.Death.ToString()) == shortNameHash)
            {
                SimpleAnimationStateBehaviour.OnAnimationCompleted -= HandleOnAnimationCompleted;
                Core.Death();
            }
        }
    }
}