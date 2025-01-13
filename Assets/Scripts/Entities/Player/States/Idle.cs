using Controllers.Player;
using UnityEngine;

namespace Entities.Player.States
{
    public sealed class Idle : PlayerState
    {
        public Idle(PlayerCore core, PlayerFactory factory) : base(core, factory)
        {
            IsRootState = true;
        }

        public override void Enter()
        {
            base.Enter();
            
            Debug.Log("entered the idle state");
            SubState = Core.Factory.GetState(typeof(Wield));
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(Run), () => PlayerInputController.MovementDirection != Vector3.zero);
            
            AddSubTransition(typeof(Charge), () => PlayerInputController.AttackKeyPressed);
        }
    }
}