using Entities.Enemies.States;
using HFSM;

namespace Entities.Enemies
{
    public class EnemyStateFactory : StateFactory
    {
        private EnemyCore Core { get; }

        public EnemyStateFactory(EnemyCore core)
        {
            Core = core;
        }

        protected override void SetStates()
        {
            AddState(typeof(EnemyIdle), new EnemyIdle(Core));
            AddState(typeof(EnemyWalk), new EnemyWalk(Core));
            AddState(typeof(EnemyHurt), new EnemyHurt(Core));
        }
    }
}