using UnityEngine;
using Utility;

namespace Entities.Player.States.Child
{
    public class PlayerChargeWalk : PlayerState
    {
        public PlayerChargeWalk(PlayerCore core) : base(core)
        {
        }

        protected override void OnUpdate()
        {
            Core.Movement.Accelerate(Core.Data.chargeSpeed);
            Core.Movement.Decelerate();
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerChargeIdle), () => Core.Body.linearVelocity == Vector2.zero && PlayerInput.MovementDirection == Vector3.zero);
            AddTransition(typeof(PlayerChargeWalk), () => PlayerInput.MovementDirection != Vector3.zero);
        }
    }    
}

