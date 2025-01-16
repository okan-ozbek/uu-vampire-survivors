using UnityEngine;
using Utility;

namespace Entities.Player.States.Child
{
    public class PlayerChargeIdle : PlayerState
    {
        public PlayerChargeIdle(PlayerCore core) : base(core)
        {
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerChargeWalk), () => PlayerInput.MovementDirection != Vector3.zero);
        }
    }
}