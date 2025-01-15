using Utility;
using UnityEngine;

namespace Entities.Player.States.Child
{
    public class PlayerRun : PlayerState
    {
        private Vector3 _velocity;
        
        public PlayerRun(PlayerCore core) : base(core)
        {
        }
        
        protected override void OnEnter()
        {
            _velocity = Core.Body.linearVelocity;
        }

        protected override void OnUpdate()
        {
            Accelerate();
            Decelerate();
            Brake();
            
            Core.Body.linearVelocity = _velocity;
            
            Flip();
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerWalk), () => PlayerInput.RunKeyHeld == false);
        }
        
        private void Flip()
        {
            Vector3 localScale = Core.transform.localScale;

            localScale.x = Core.Body.linearVelocity.x switch
            {
                > 0.0f => Mathf.Abs(localScale.x),
                < 0.0f => -Mathf.Abs(localScale.x),
                _ => localScale.x
            };

            Core.transform.localScale = localScale;
        }
        
        private void Accelerate()
        {
            if (PlayerInput.MovementDirection.x != 0)
            {
                if (Mathf.Abs(Core.Body.linearVelocity.x) > Core.Data.maxSpeed)
                {
                    SetMoveTowardsX(Core.Data.decelerationSpeed * Time.deltaTime);
                }
                else
                {
                    _velocity.x += PlayerInput.NormalizedMovementDirection.x * Core.Data.accelerationSpeed * Time.deltaTime;
                }
            }
            
            if (PlayerInput.MovementDirection.y != 0)
            {
                if (Mathf.Abs(Core.Body.linearVelocity.y) > Core.Data.maxSpeed)
                {
                    SetMoveTowardsY(Core.Data.decelerationSpeed * Time.deltaTime);
                }
                else
                {
                    _velocity.y += PlayerInput.NormalizedMovementDirection.y * Core.Data.accelerationSpeed * Time.deltaTime;
                }
            }
        }

        private void Decelerate()
        {
            if (PlayerInput.MovementDirection.x == 0)
            {
                SetMoveTowardsX(Core.Data.decelerationSpeed * Time.deltaTime);
            }
            
            if (PlayerInput.MovementDirection.y == 0)
            {
                SetMoveTowardsY(Core.Data.decelerationSpeed * Time.deltaTime);
            }
        }

        private void Brake()
        {
            if (PlayerInput.MovementDirection.x != 0 && PlayerInput.MovementDirection.x != Mathf.Sign(_velocity.x))
            {
                SetMoveTowardsX(Core.Data.brakeSpeed * Time.deltaTime);
            }
            
            if (PlayerInput.MovementDirection.y != 0 && PlayerInput.MovementDirection.y != Mathf.Sign(_velocity.y))
            {
                SetMoveTowardsY(Core.Data.brakeSpeed * Time.deltaTime);
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