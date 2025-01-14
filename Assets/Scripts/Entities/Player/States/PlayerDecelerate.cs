using Controllers.Player;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerDecelerate : PlayerState
    {
        private Vector3 _velocity;
        
        public PlayerDecelerate(PlayerCore core) : base(core)
        {
        }

        protected override void OnEnter()
        {
            _velocity = Core.Body.linearVelocity;
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            Decelerate();
            
            Core.Body.linearVelocity = _velocity;
        }

        protected override void SetTransitions()
        {
        }

        protected override void SetChildTransitions()
        {
        }
        
        private void Decelerate()
        {
            if (PlayerInputController.MovementDirection.x == 0)
            {
                SetMoveTowardsX(Core.Data.decelerationSpeed * Time.deltaTime);
            }
            
            if (PlayerInputController.MovementDirection.y == 0)
            {
                SetMoveTowardsY(Core.Data.decelerationSpeed * Time.deltaTime);
            }
        }

        private void SetMoveTowardsX(float maxDelta)
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, 0.0f, maxDelta);   
        }
        
        private void SetMoveTowardsY(float maxDelta)
        {
            _velocity.y = Mathf.MoveTowards(_velocity.y, 0.0f, maxDelta);   
        }
    }
}