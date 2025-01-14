﻿using Configs;
using Controllers;
using Controllers.Player;
using UnityEngine;

namespace Entities.Player.States
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
            ChildState = Core.StateFactory.GetState(typeof(PlayerIdle));
            ChildState.ParentState = this;
            ChildState.Enter();
            
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
            if (_timeController.TimePassed > ReduceMaxSpeedBufferTime && Core.Data.maxSpeed == _maxSpeed)
            {
                Core.Data.maxSpeed *= 0.5f;
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
        
        protected override void SetChildTransitions()
        {
            AddChildTransition(typeof(PlayerIdle), () => PlayerInputController.MovementDirection == Vector3.zero && Core.Body.linearVelocity.magnitude == 0);
            AddChildTransition(typeof(PlayerRun), () => PlayerInputController.MovementDirection != Vector3.zero && Core.Body.linearVelocity.magnitude > 0);
        }
    }
}