using Utility;
using UnityEngine;

namespace Entities.Player.States.Child
{
    public class PlayerIdle : PlayerState
    {
        public PlayerIdle(PlayerCore core) : base(core)
        {
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerMove), () => PlayerInput.MovementDirection != Vector3.zero);
        }
    }
}