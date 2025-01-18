using Configs;
using Unity;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class EnemyIdle : EnemyState
    {
        private bool IsHurt { get; set; }
        
        public EnemyIdle(EnemyCore core) : base(core)
        {
            EnableRootState();
        }

        protected override void OnEnter()
        {
            Core.Animator.PlayAnimation(EnemyAnimationType.Idle);
        }
        
        protected override void SetTransitions()
        {
            //AddTransition(typeof(EnemyWalk), () => PlayerInput.MovementDirection != Vector3.zero);
            AddTransition(typeof(EnemyHurt), () => Core.Data.isHurt);
        }
    }
}