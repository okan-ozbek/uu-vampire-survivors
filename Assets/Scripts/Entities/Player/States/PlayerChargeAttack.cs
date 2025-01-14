using Configs;
using Controllers;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerChargeAttack : PlayerState
    {
        private readonly TimeController _timeController;
        private bool _isFinished;
        private bool _canTransition;
        
        public PlayerChargeAttack(PlayerCore core) : base(core)
        {
            EnableRootState();
            
            _timeController = new TimeController(1f);
        }
        
        protected override void OnEnter()
        {
            ChildState = Core.StateFactory.GetState(typeof(PlayerIdle));
            ChildState.ParentState = this;
            ChildState.Enter();
            
            _canTransition = false;
        }
        
        protected override void OnExit()
        {
            _timeController.Reset();

            _isFinished = false;
            _canTransition = false;
        }
        
        protected override void OnUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _canTransition = true;
            }
            
            _timeController.Update();
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
        
        protected override void SetChildTransitions()
        {
            AddChildTransition(typeof(PlayerIdle), () => new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).magnitude == 0);
            AddChildTransition(typeof(PlayerRun), () => new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).magnitude > 0);
        }
    }
}