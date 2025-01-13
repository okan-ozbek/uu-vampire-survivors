using Controllers.Player;

namespace Entities.Player.States
{
    public class Wield : PlayerState
    {
        public Wield(PlayerCore core) : base(core)
        {
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(Charge), () => PlayerInputController.AttackKeyPressed);
        }
    }
}