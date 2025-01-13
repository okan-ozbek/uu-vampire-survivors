using Entities.Player.States;
using FSM;

namespace Entities.Player
{
    public sealed class PlayerFactory : StateFactory
    {
        private readonly PlayerCore _core;

        public PlayerFactory(PlayerCore core)
        {
            _core = core;
            SetStates();
        }

        protected override void SetStates()
        {
            AddState(new Idle(_core));
            AddState(new Run(_core));
            AddState(new Dash(_core));
            AddState(new Wield(_core));
            AddState(new Charge(_core));
            AddState(new BasicAttack(_core));
            AddState(new HeavyAttack(_core));
        }
    }
}