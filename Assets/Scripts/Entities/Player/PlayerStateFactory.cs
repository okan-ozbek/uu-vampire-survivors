using Entities.Player.States;
using HFSM;

namespace Entities.Player
{
    public class PlayerStateFactory : StateFactory
    {
        private PlayerCore Core { get; }
        
        public PlayerStateFactory(PlayerCore core)
        {
            Core = core;
        }
        
        protected override void SetStates()
        {
            AddState(typeof(PlayerIdle), new PlayerIdle(Core));
            AddState(typeof(PlayerWalk), new PlayerWalk(Core));
            AddState(typeof(PlayerRun), new PlayerRun(Core));
            AddState(typeof(PlayerDash), new PlayerDash(Core));
            AddState(typeof(PlayerHurt), new PlayerHurt(Core));
            AddState(typeof(PlayerAttack), new PlayerAttack(Core));
        }
    }
}