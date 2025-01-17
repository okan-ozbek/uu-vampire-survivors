using HFSM;

namespace Entities.Enemies.States
{
    public abstract class EnemyState : State
    {
        protected new EnemyCore Core { get; private set; }
        
        protected EnemyState(EnemyCore core) : base(core)
        {
            Core = core;
        }
    }
}