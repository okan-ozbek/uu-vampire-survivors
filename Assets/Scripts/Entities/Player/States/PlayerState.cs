using FSM;

namespace Entities.Player.States
{
    public abstract class PlayerState : State
    {
        protected readonly PlayerCore Core;

        protected PlayerState(PlayerCore core, StateFactory factory) : base(core, factory)
        {
            Core = core;
        }
    }
}