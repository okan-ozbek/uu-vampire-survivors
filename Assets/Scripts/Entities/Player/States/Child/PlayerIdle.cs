using UnityEngine;

namespace Entities.Player.States.Child
{
    public class PlayerIdle : PlayerState
    {
        public PlayerIdle(PlayerCore core) : base(core)
        {
        }
        
        protected override void OnEnter()
        {
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerRun), () => new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).magnitude > 0);
        }

        protected override void SetChildTransitions()
        {
        }
    }
}