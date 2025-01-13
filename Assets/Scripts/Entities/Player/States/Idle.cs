using Controllers.Player;
using UnityEngine;

namespace Entities.Player.States
{
    public sealed class Idle : PlayerState
    {
        public Idle(PlayerCore core) : base(core)
        {
            IsRootState = true;
        }

        protected override void OnEnter()
        {
            SubState = Core.Factory.GetState(typeof(Wield));
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(Run), () => PlayerInputController.MovementDirection != Vector3.zero);
            
            AddSubTransition(typeof(Charge), () => PlayerInputController.AttackKeyPressed);
        }
    }
}