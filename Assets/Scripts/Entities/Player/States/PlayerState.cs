using HFSM;

namespace Entities.Player.States
{
    public abstract class PlayerState : State
    {
        protected new PlayerCore Core; 
        
        protected PlayerState(PlayerCore core) : base(core)
        {
            Core = core;
        }
    }
}