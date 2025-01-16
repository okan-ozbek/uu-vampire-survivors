using Utility;
using UnityEngine;

namespace Entities.Player.States.Child
{
    public class PlayerRun : PlayerState
    {
        public PlayerRun(PlayerCore core) : base(core)
        {
        }

        protected override void OnUpdate()
        {
            Core.Movement.Accelerate(Core.Data.runSpeed);
            Core.Movement.Decelerate();
            Core.Movement.Brake();
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerWalk), () => PlayerInput.RunKeyHeld == false);
        }
    }
}