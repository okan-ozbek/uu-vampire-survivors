using Controllers.Player;

namespace Entities.Player.States
{
    public class Wield : PlayerState
    {
        public Wield(PlayerCore core, PlayerFactory factory) : base(core, factory)
        {
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(Charge), () => PlayerInputController.AttackKeyPressed);
        }
    }
}