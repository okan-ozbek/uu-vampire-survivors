using Controllers.Player;
using UnityEngine;

namespace Entities.Player.States
{
    public sealed class Idle : PlayerState
    {
        public Idle(PlayerCore core) : base(core)
        {
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(Run), () => PlayerInputController.MovementDirection != Vector3.zero);
            AddTransition(typeof(Charge), () => PlayerInputController.AttackKeyPressed);
        }
    }
}