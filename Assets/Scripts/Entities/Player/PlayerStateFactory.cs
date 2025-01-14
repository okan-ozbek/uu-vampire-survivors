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
            // Combat
            AddState(typeof(PlayerChargeAttack), new PlayerChargeAttack(Core));
            AddState(typeof(PlayerBasicAttack), new PlayerBasicAttack(Core));
            AddState(typeof(PlayerHeavyAttack), new PlayerHeavyAttack(Core));
            
            // Locomotion
            AddState(typeof(PlayerLocomotion), new PlayerLocomotion(Core));
            AddState(typeof(PlayerIdle), new PlayerIdle(Core));
            AddState(typeof(PlayerRun), new PlayerRun(Core));
            AddState(typeof(PlayerDash), new PlayerDash(Core));
        }
    }
}