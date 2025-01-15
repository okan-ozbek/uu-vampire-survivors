using Controllers.Player;
using UnityEngine;

namespace Entities.Player.States.Child
{
    public class PlayerWalk : PlayerState
    {
        private Vector3 _velocity;
        
        private float _baseMaxSpeed;
        
        public PlayerWalk(PlayerCore core) : base(core)
        {
        }
        
        protected override void OnEnter()
        {
            _velocity = Core.Body.linearVelocity;
            
            _baseMaxSpeed = Core.Data.maxSpeed;
        }

        protected override void OnUpdate()
        {
            Core.Data.maxSpeed = Mathf.Clamp(Core.Data.maxSpeed, 1.5f, _baseMaxSpeed * 0.5f);
            
            Accelerate();
            Decelerate();
            Brake();
            
            Core.Body.linearVelocity = _velocity;
            
            Flip();
        }
        
        protected override void OnExit()
        {
            Core.Data.maxSpeed = _baseMaxSpeed;
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerRun), () => PlayerInputController.RunKeyHeld);
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
            if (PlayerInputController.MovementDirection.x != 0)
            {
                if (Mathf.Abs(Core.Body.linearVelocity.x) > Core.Data.maxSpeed)
                {
                    SetMoveTowardsX(Core.Data.decelerationSpeed * Time.deltaTime);
                }
                else
                {
                    _velocity.x += PlayerInputController.NormalizedMovementDirection.x * Core.Data.accelerationSpeed * Time.deltaTime;
                }
            }
            
            if (PlayerInputController.MovementDirection.y != 0)
            {
                if (Mathf.Abs(Core.Body.linearVelocity.y) > Core.Data.maxSpeed)
                {
                    SetMoveTowardsY(Core.Data.decelerationSpeed * Time.deltaTime);
                }
                else
                {
                    _velocity.y += PlayerInputController.NormalizedMovementDirection.y * Core.Data.accelerationSpeed * Time.deltaTime;
                }
            }
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

        private void Brake()
        {
            if (PlayerInputController.MovementDirection.x != 0 && PlayerInputController.MovementDirection.x != Mathf.Sign(_velocity.x))
            {
                SetMoveTowardsX(Core.Data.brakeSpeed * Time.deltaTime);
            }
            
            if (PlayerInputController.MovementDirection.y != 0 && PlayerInputController.MovementDirection.y != Mathf.Sign(_velocity.y))
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