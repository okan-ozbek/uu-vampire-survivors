using Configs;
using Controllers;
using Controllers.Player;

namespace Entities.Player.States
{
    public class Charge : PlayerState
    {
        private const float ChargeTime = 1f;
        
        private bool _isFinished;
        private int _chargeLevel;

        private readonly TimeController _timer;
        
        public Charge(PlayerCore core, PlayerFactory factory) : base(core, factory)
        {
            _timer = new TimeController(ChargeTime);
        }

        public override void Enter()
        {
            base.Enter();
            
            _isFinished = false;
        }

        public override void Update()
        {
            base.Update();
            
            _isFinished = PlayerInputController.AttackKeyReleased;
            
            ChargeTimer();
        }

        public override void Exit()
        {
            base.Exit();
            
            ResetParameters();
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(BasicAttack), () => _isFinished && _chargeLevel == 0);
            AddTransition(typeof(HeavyAttack), () => _isFinished && _chargeLevel == 1);
        }

        private void ResetParameters()
        {
            _isFinished = false;
            _chargeLevel = 0;
            
            _timer.Reset();
        }

        private void ChargeTimer()
        {
            if (_isFinished == false)
            {
                _timer.Update();
                if (_timer.IsFinished() && _chargeLevel == 0)
                {
                    _chargeLevel++;
                    PlayerEventConfig.OnPlayerChargeAttack?.Invoke(Core.Data.Guid, _chargeLevel);
                }
            }
        }
    }
}