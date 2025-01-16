using UnityEngine;
using Utility;

namespace Entities.Player
{

    public class PlayerMovement
    {
        private PlayerCore Core { get; }
        
        private Vector3 LinearVelocity
        {
            get => Core.Body.linearVelocity;
            set => Core.Body.linearVelocity = value;
        }

        public PlayerMovement(PlayerCore core)
        {
            Core = core;
        }

        public void Flip()
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

        public void Accelerate(float maxSpeed)
        {
            if (PlayerInput.MovementDirection.x != 0)
            {
                if (Mathf.Abs(Core.Body.linearVelocity.x) > maxSpeed)
                {
                    SetMoveTowardsX(Core.Data.decelerationSpeed * Time.deltaTime);
                }
                else
                {
                    Vector3 velocity = LinearVelocity;
                    velocity.x += PlayerInput.NormalizedMovementDirection.x * Core.Data.accelerationSpeed * Time.deltaTime;
                    LinearVelocity = velocity;
                }
            }
            
            if (PlayerInput.MovementDirection.y != 0)
            {
                if (Mathf.Abs(Core.Body.linearVelocity.y) > maxSpeed)
                {
                    SetMoveTowardsY(Core.Data.decelerationSpeed * Time.deltaTime);
                }
                else
                {
                    Vector3 velocity = LinearVelocity;
                    velocity.y += PlayerInput.NormalizedMovementDirection.y * Core.Data.accelerationSpeed * Time.deltaTime;
                    LinearVelocity = velocity;
                }
            }
        }

        public void Decelerate()
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

        public void Brake()
        {
            if (PlayerInput.MovementDirection.x != 0 && PlayerInput.MovementDirection.x != Mathf.Sign(LinearVelocity.x))
            {
                SetMoveTowardsX(Core.Data.brakeSpeed * Time.deltaTime);
            }
            
            if (PlayerInput.MovementDirection.y != 0 && PlayerInput.MovementDirection.y != Mathf.Sign(LinearVelocity.y))
            {
                SetMoveTowardsY(Core.Data.brakeSpeed * Time.deltaTime);
            }
        }

        private void SetMoveTowardsX(float maxDelta)
        {
            Vector3 velocity = LinearVelocity;
            velocity.x = Mathf.MoveTowards(velocity.x, 0.0f, maxDelta);  
            LinearVelocity = velocity;
        }
        
        private void SetMoveTowardsY(float maxDelta)
        {
            Vector3 velocity = LinearVelocity;
            velocity.y = Mathf.MoveTowards(velocity.y, 0.0f, maxDelta);   
            LinearVelocity = velocity;
        }
    }
}