using HFSM;

namespace Entities.Player.States
{
    public abstract class PlayerState : State
    {
        protected PlayerCore Core { get; private set; }

        protected PlayerState(PlayerCore core)
        {
            Core = core;
        }
    }
}