using Configs;
using Controllers;
using Controllers.Player;
using Entities.Player.States.Child;
using UnityEngine;

namespace Entities.Player.States.Root
{
    public class PlayerChargeAttack : PlayerState
    {
        private const float ReduceMaxSpeedBufferTime = 0.15f;
        
        private readonly TimeController _timeController;
        private bool _isFinished;
        private bool _canTransition;
        private float _maxSpeed;
        
        public PlayerChargeAttack(PlayerCore core) : base(core)
        {
            EnableRootState();
            
            _timeController = new TimeController(1f);
        }
        
        protected override void OnEnter()
        {
            SetChild(typeof(PlayerIdle));
            
            _maxSpeed = Core.Data.maxSpeed;
            _canTransition = false;
        }
        
        protected override void OnExit()
        {
            _timeController.Reset();

            _isFinished = false;
            _canTransition = false;
            Core.Data.maxSpeed = _maxSpeed;
        }
        
        protected override void OnUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _canTransition = true;
            }
            
            _timeController.Update();
            if (_timeController.TimePassed > ReduceMaxSpeedBufferTime)
            {
                Core.Data.maxSpeed = 1.5f;
            }
            
            if (_timeController.IsFinished() && _isFinished == false)
            {
                _isFinished = true;
                PlayerEventConfig.OnPlayerChargeAttack.Invoke(Core.Data.Guid, 1);
            }
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerLocomotion), () => Input.GetKeyDown(KeyCode.Mouse1));
            AddTransition(typeof(PlayerBasicAttack), () => _canTransition && _isFinished == false);
            AddTransition(typeof(PlayerHeavyAttack), () => _canTransition && _isFinished);
        }
    }
}