using Controllers.Player;
using Entities.Player.States.Child;
using UnityEngine;

namespace Entities.Player.States.Root
{
    public class PlayerLocomotion : PlayerState
    {
        public PlayerLocomotion(PlayerCore core) : base(core)
        {
            EnableRootState();
        }
        
        protected override void OnEnter()
        {
            SetChild(typeof(PlayerIdle));
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerChargeAttack), () => Input.GetKeyDown(KeyCode.Mouse0));
        }
    }
}