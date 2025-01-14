using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerRun : PlayerState
    {
        public PlayerRun(PlayerCore core) : base(core)
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
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f).normalized;
            Core.transform.Translate(direction * 7f * Time.deltaTime);
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerDash), () => Input.GetKeyDown(KeyCode.Space));
            AddTransition(typeof(PlayerIdle), () => new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).magnitude == 0);
            
        }

        protected override void SetChildTransitions()
        {
        }
    }
}