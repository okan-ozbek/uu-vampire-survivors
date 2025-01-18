using UnityEngine;

namespace Entities.Enemies.States
{
    public class EnemyWalk : EnemyState
    {
        private readonly Vector2 _randomPositionMinMaxX = new(-4, 3);
        private readonly Vector2 _randomPositionMinMaxY = new(-4, 3);
        
        private Vector2 _randomPosition;
        private Vector2 _velocity;
        
        public EnemyWalk(EnemyCore core) : base(core)
        {
            EnableRootState();
        }
        
        protected override void OnEnter()
        {
            Core.Animator.PlayAnimation(EnemyAnimationType.Walk);
            Core.Body.linearVelocity = Vector2.zero;
            
            _randomPosition = new Vector2(
                Random.Range(_randomPositionMinMaxX.x, _randomPositionMinMaxX.y),
                Random.Range(_randomPositionMinMaxY.x, _randomPositionMinMaxY.y)
            );
        }
        
        protected override void OnUpdate()
        {
            Move();
            Flip();
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(EnemyHurt), () => Core.Data.isHurt);
            AddTransition(typeof(EnemyIdle), () => Vector2.Distance(Core.transform.position, _randomPosition) < 0.1f);
        }

        private void Move()
        {
            Core.transform.position = Vector2.SmoothDamp(
                Core.transform.position,
                _randomPosition,
                ref _velocity,
                0.3f,
                Core.Data.walkSpeed
            );
        }
        
        private void Flip()
        {
            Vector3 localScale = Core.transform.localScale;
            
            localScale.x = _velocity.x switch
            {
                > 0.0f => Mathf.Abs(localScale.x),
                < 0.0f => -Mathf.Abs(localScale.x),
                _ => localScale.x
            };
            
            Core.transform.localScale = localScale;
        }
    }
}