using System.Collections;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class EnemyIdle : EnemyState
    {
        private bool IsWaiting { get; set; }
        
        public EnemyIdle(EnemyCore core) : base(core)
        {
            EnableRootState();
        }

        protected override void OnEnter()
        {
            IsWaiting = true;
            
            Core.Animator.PlayAnimation(EnemyAnimationType.Idle);
            Core.StartCoroutine(WaitRoutine());
        }
        
        protected override void OnExit()
        {
            IsWaiting = true;
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(EnemyWalk), () => IsWaiting == false);
            AddTransition(typeof(EnemyHurt), () => Core.Data.isHurt);
        }
        
        private IEnumerator WaitRoutine()
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            IsWaiting = false;
        }
    }
}