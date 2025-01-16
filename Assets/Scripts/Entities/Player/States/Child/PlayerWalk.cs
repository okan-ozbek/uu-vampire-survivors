using Utility;
using UnityEngine;

namespace Entities.Player.States.Child
{
    public class PlayerWalk : PlayerState
    {
        public PlayerWalk(PlayerCore core) : base(core)
        {
        }

        protected override void OnUpdate()
        {
            Core.Movement.Accelerate(Core.Data.walkSpeed);
            Core.Movement.Decelerate();
            Core.Movement.Brake();
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerRun), () => PlayerInput.RunKeyHeld);
        }
    }
}