using UnityEditor;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerLocomotion : PlayerState
    {
        public PlayerLocomotion(PlayerCore core) : base(core)
        {
            EnableRootState();
        }
        
        protected override void OnEnter()
        {
            ChildState = Core.StateFactory.GetState(typeof(PlayerIdle));
            ChildState.ParentState = this;
            ChildState.Enter();
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