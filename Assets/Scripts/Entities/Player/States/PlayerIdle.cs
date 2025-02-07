using HFSM;
using UnityEngine;
using Utility;

namespace Entities.Player.States
{
    public class PlayerIdle : PlayerState
    {
        public PlayerIdle(PlayerCore core) : base(core)
        {
            EnableRootState();
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerMove), () => PlayerInput.MovementDirection != Vector2.zero);
        }
    }
}