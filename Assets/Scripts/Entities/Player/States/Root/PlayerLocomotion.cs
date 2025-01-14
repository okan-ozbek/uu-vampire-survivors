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
        
        protected override void OnExit()
        {
        }
        
        protected override void OnUpdate()
        {
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerChargeAttack), () => Input.GetKeyDown(KeyCode.Mouse0));
        }

        protected override void SetChildTransitions()
        {
            AddChildTransition(typeof(PlayerIdle), () => new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).magnitude == 0);
            AddChildTransition(typeof(PlayerRun), () => new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).magnitude > 0);
        }
    }
}