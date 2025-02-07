using Entities.Player.States;
using HFSM;

namespace Entities.Player.Factories
{
    public class PlayerStateFactory : StateFactory
    {
        private readonly PlayerCore _core;
        
        public PlayerStateFactory(PlayerCore core) 
        {
            _core = core;
        }
        
        protected override void SetStates()
        {
            AddState(typeof(PlayerIdle), new PlayerIdle(_core));
            AddState(typeof(PlayerMove), new PlayerMove(_core));
        }
    }
}