using Entities.Player;
using UnityEngine;

namespace Entities.Enemies
{
    public enum EnemyAnimationType
    {
        Idle,
        Walk,
        Hurt,
        Death,
    }
    
    public class EnemyAnimator
    {
        private Animator Animator { get; set; }
        private EnemyAnimationType CurrentAnimationType { get; set; }

        public EnemyAnimator(Animator animator)
        {
            Animator = animator;
        }

        public void PlayAnimation(EnemyAnimationType animationType)
        {
            if (animationType != CurrentAnimationType)
            {
                CurrentAnimationType = animationType;
                Animator.Play(animationType.ToString());
            }
        }
    }
}