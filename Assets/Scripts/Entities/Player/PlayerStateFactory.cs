using Entities.Player.States;
using Entities.Player.States.Child;
using Entities.Player.States.Root;
using HFSM;
using Unity.VisualScripting;

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
            AddState(typeof(PlayerDecelerate), new PlayerDecelerate(Core));
            AddState(typeof(PlayerChargeIdle), new PlayerChargeIdle(Core));
            AddState(typeof(PlayerChargeWalk), new PlayerChargeWalk(Core));

            // Locomotion
            AddState(typeof(PlayerLocomotion), new PlayerLocomotion(Core));
            AddState(typeof(PlayerMove), new PlayerMove(Core));
            AddState(typeof(PlayerIdle), new PlayerIdle(Core));
            AddState(typeof(PlayerWalk), new PlayerWalk(Core));
            AddState(typeof(PlayerRun), new PlayerRun(Core));
            AddState(typeof(PlayerDash), new PlayerDash(Core));
        }
    }
}