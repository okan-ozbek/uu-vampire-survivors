using UnityEngine;
using Utility;

namespace Entities.Player.States
{
    public class PlayerMove : PlayerState
    {
        public PlayerMove(PlayerCore core) : base(core)
        {
            EnableRootState();
        }

        protected override void OnUpdate()
        {
            // TODO remove later for proper movement
            Core.transform.Translate(PlayerInput.NormalizedMovementDirection * 10 * Time.deltaTime);
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerIdle), () => PlayerInput.MovementDirection == Vector2.zero);
        }
    }
}