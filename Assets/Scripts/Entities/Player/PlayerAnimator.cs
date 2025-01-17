using UnityEngine;

namespace Entities.Player
{
    public enum PlayerAnimationType
    {
        Attack,
        Hurt,
        Idle,
        Walk,
        Run,
        Dash
    }
    
    public class PlayerAnimator
    {
        private Animator Animator { get; set; }
        private PlayerAnimationType CurrentAnimationType { get; set; }

        public PlayerAnimator(Animator animator)
        {
            Animator = animator;
        }

        public void PlayAnimation(PlayerAnimationType animationType)
        {
            if (animationType != CurrentAnimationType)
            {
                CurrentAnimationType = animationType;
                Animator.Play(animationType.ToString());
            }
        }
    }
}