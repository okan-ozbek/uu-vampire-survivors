using Configs;
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

        protected override void OnEnter()
        {
            Core.Animator.PlayAnimation(PlayerAnimationType.Idle);
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerRun), () => PlayerInput.MovementDirection != Vector3.zero);
            AddTransition(typeof(PlayerHurt), () => PlayerInput.HurtKeyPressed);
            AddTransition(typeof(PlayerAttack), () => PlayerInput.AttackKeyPressed);
        }
    }
}