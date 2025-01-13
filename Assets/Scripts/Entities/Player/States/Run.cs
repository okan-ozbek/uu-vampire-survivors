using Controllers.Player;
using UnityEngine;

namespace Entities.Player.States
{
    public sealed class Run : PlayerState
    {
        private Vector3 _velocity;

        public Run(PlayerCore core) : base(core)
        {
            IsRootState = true;
        }

        protected override void OnEnter()
        {
            SubState = Core.Factory.GetState(typeof(Wield));
            
            _velocity = Core.Body.linearVelocity;
        }

        protected override void OnUpdate()
        {
            Accelerate();
            Decelerate();
            Brake();
            
            Core.Body.linearVelocity = _velocity;
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(Idle), () => PlayerInputController.MovementDirection == Vector3.zero && _velocity == Vector3.zero);
            AddTransition(typeof(Dash), () => Core.Data.canDash && PlayerInputController.DashKeyPressed && PlayerInputController.MovementDirection != Vector3.zero && _velocity != Vector3.zero);
            
            AddSubTransition(typeof(Charge), () => PlayerInputController.AttackKeyPressed);
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