using HFSM;

namespace Entities.Player.States
{
    public abstract class PlayerState : State
    {
        protected new PlayerCore Core { get; private set; }

        protected PlayerState(PlayerCore core) : base(core)
        {
            Core = core;
        }
    }
}