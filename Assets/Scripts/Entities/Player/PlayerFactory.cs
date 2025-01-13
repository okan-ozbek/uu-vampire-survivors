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
            AddState(new Idle(_core, this));
            AddState(new Run(_core, this));
            AddState(new Dash(_core, this));
            AddState(new Wield(_core, this));
            AddState(new Charge(_core, this));
            AddState(new BasicAttack(_core, this));
            AddState(new HeavyAttack(_core, this));
        }
    }
}