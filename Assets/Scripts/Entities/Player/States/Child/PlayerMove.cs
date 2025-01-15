using Controllers.Player;
using UnityEngine;

namespace Entities.Player.States.Child
{
    public class PlayerMove : PlayerState
    {
        public PlayerMove(PlayerCore core) : base(core)
        {
        }

        protected override void OnEnter()
        {
            SetChild(typeof(PlayerWalk));
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerDash), () => Input.GetKeyDown(KeyCode.Space) && Core.Data.canDash && PlayerInputController.MovementDirection != Vector3.zero);
            AddTransition(typeof(PlayerIdle), () => Core.Body.linearVelocity == Vector2.zero && PlayerInputController.MovementDirection == Vector3.zero);
        }
    }
}