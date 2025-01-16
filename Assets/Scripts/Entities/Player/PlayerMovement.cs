using UnityEngine;
using Utility;

namespace Entities.Player
{

    public class PlayerMovement
    {
        private PlayerCore Core { get; }

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

        public void Accelerate(float speed)
        {
            if (PlayerInput.MovementDirection.x != 0)
            {
                if (Mathf.Abs(Core.Body.linearVelocity.x) > speed)
                {
                    SetMoveTowardsX(Core.Data.decelerationSpeed * Time.deltaTime);
                }
                else
                {
                    Vector3 velocity = Core.Body.linearVelocity;
                    velocity.x += PlayerInput.NormalizedMovementDirection.x * Core.Data.accelerationSpeed * Time.deltaTime;
                    Core.Body.linearVelocity = velocity;
                }
            }
            
            if (PlayerInput.MovementDirection.y != 0)
            {
                if (Mathf.Abs(Core.Body.linearVelocity.y) > speed)
                {
                    SetMoveTowardsX(Core.Data.decelerationSpeed * Time.deltaTime);
                }
                else
                {
                    Vector3 velocity = Core.Body.linearVelocity;
                    velocity.y += PlayerInput.NormalizedMovementDirection.y * Core.Data.accelerationSpeed * Time.deltaTime;
                    Core.Body.linearVelocity = velocity;
                }
            }
        }

        public void Decelerate()
        {
            if (Core.Body.linearVelocity.x != 0)
            {
                SetMoveTowardsX(Core.Data.decelerationSpeed * Time.deltaTime);
            }

            if (Core.Body.linearVelocity.y != 0)
            {
                SetMoveTowardsY(Core.Data.decelerationSpeed * Time.deltaTime);
            }
        }

        private void SetMoveTowardsX(float maxDelta)
        {
            Vector3 velocity = Core.Body.linearVelocity;
            velocity.x = Mathf.MoveTowards(Core.Body.linearVelocity.x, 0.0f, maxDelta);
            Core.Body.linearVelocity = velocity;
        }

        private void SetMoveTowardsY(float maxDelta)
        {
            Vector3 velocity = Core.Body.linearVelocity;
            velocity.y = Mathf.MoveTowards(Core.Body.linearVelocity.y, 0.0f, maxDelta);
            Core.Body.linearVelocity = velocity;
        }
    }
}